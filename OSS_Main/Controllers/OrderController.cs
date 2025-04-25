using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OSS_Main.Hubs;
using OSS_Main.Models.DTO.GHN;
using OSS_Main.Models.Entity;
using OSS_Main.Proxy.GHN;
using OSS_Main.Service.Implementation;
using OSS_Main.Service.Interface;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace OSS_Main.Controllers
{
    [Authorize(Roles ="Customer")]
    public class OrderController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IGhnProxy _ghnService;
        private readonly IReceiveInforService _receiveInforService;
        private readonly Prn222ProjectContext _context; // chỗ này bị lười cập nhật ở service:)))
        private readonly IHubContext<OrderHub> _hubContext;
        public OrderController(ICartService cartService, 
                                            IOrderService orderService,
                                            IUserService userService, 
                                            IProductService productService, 
                                            IGhnProxy ghnService, 
                                            IConfiguration configuration,
                                            IReceiveInforService receiveInforService,
                                            Prn222ProjectContext context,
                                            IHubContext<OrderHub> hubContext)
        {
            _cartService = cartService;
            _orderService = orderService;
            _userService = userService;
            _productService = productService;
            _ghnService = ghnService;
            _configuration = configuration;
            _receiveInforService = receiveInforService;
            _context = context;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> OrderConfirmation(string selectedItems)
        {

            if (string.IsNullOrEmpty(selectedItems))
            {
                TempData["Error"] = "No items available for the order confirmation.";
                return RedirectToAction("Index", "Cart");
            }

            var selectedCartId = JsonConvert.DeserializeObject<List<int>>(selectedItems);
            var selectedCartItems = await _cartService.GetCartItemsByIdsAsync(selectedCartId);
            decimal totalAmount = 0;
            foreach (var item in selectedCartItems)
            {
                totalAmount += item.PriceEachItem * item.Quantity;
            }

            ViewBag.TotalAmount = totalAmount;

            var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var receiverInfo = await _receiveInforService.GetDefaultReceiverInfoAsync(customerId);

            // Gọi phương thức GetReceiverInformation để lấy địa chỉ
            var receiverInfoList = await _context.ReceiverInformations
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();

            bool hasAddress = receiverInfoList.Any(); // Kiểm tra nếu có địa chỉ

            ViewBag.HasAddress = hasAddress; // Truyền thông tin vào view để kiểm tra

            // Nếu có địa chỉ mặc định, gán vào ViewBag để sử dụng trong View
            if (receiverInfo != null)
            {
                ViewBag.FullName = receiverInfo.FullName;
                ViewBag.Address = $"{receiverInfo.Address}, {receiverInfo.WardName_GHN}, {receiverInfo.DistrictName_GHN}, {receiverInfo.ProvinceName_GHN}";
                ViewBag.ReceiverID = receiverInfo.ReceiverId;

                ViewBag.WardId_GHN = receiverInfo.WardId_GHN;
                ViewBag.DistrictId_GHN = receiverInfo.DistrictId_GHN;
                ViewBag.ProvinceId_GHN = receiverInfo.ProvinceId_GHN;

                ViewBag.PhoneNumber = receiverInfo.PhoneNumber;
                ViewBag.Email = receiverInfo.Email;
            }

            return View(selectedCartItems);
        }


        [HttpGet]
        public async Task<IActionResult> GetProvinces()
        {
            var result = await _ghnService.GetProvincesAsync();
            return Content(result, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetDistricts(int provinceId)
        {
            var result = await _ghnService.GetDistrictsAsync(provinceId);
            return Content(result, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetWards(int districtId)
        {
            var result = await _ghnService.GetWardsAsync(districtId);
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> CalculateShipping([FromBody] ShippingRequest request)
        {
            var result = await _ghnService.CalculateShippingFeeAsync(
                request.shopId, request.fromDistrictId, request.toDistrictId,
                request.weight, request.length, request.width, request.height,
                request.service_id, request.service_type_id);
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> GetAvailableServices([FromBody] AvailableServicesRequest request)
        {
            var shopIdString = _configuration["GHNSettings:ShopId"]; 
            if (int.TryParse(shopIdString, out int shopId)) 
            {
                var result = await _ghnService.GetAvailableServicesAsync(shopId, request.fromDistrictId, request.toDistrictId);
                return Content(result, "application/json");
            }
            else
            {
                return BadRequest("Invalid ShopId.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddReceiverInformation([FromBody] ReceiverInformation address)
        {
            if (address == null)
            {
                return BadRequest("Invalid address data.");
            }

            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(address);

            // Validate the model
            bool isValid = Validator.TryValidateObject(address, context, validationResults, true);

            if (!isValid)
            {
                return BadRequest(new { errors = validationResults });
            }

            var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Nếu địa chỉ mới được chọn là IsDefault, cập nhật tất cả địa chỉ khác thành false
            if (address.IsDefault == true)
            {
                var existingAddresses = await _context.ReceiverInformations
                    .Where(r => r.CustomerId == customerId)
                    .ToListAsync();

                foreach (var existingAddress in existingAddresses)
                {
                    existingAddress.IsDefault = false;
                }

                await _context.SaveChangesAsync();
            }

            address.CustomerId = customerId;
            await _receiveInforService.AddReceiveInformationAsync(address);

            return Json(new { success = true });
        }



        [HttpGet]
        public async Task<IActionResult> GetReceiverInformation()
        {
            var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var receiverInfoList = await _context.ReceiverInformations
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();

            var result = receiverInfoList.Select(r => new
            {
                r.ReceiverId,
                r.FullName,
                r.Address,
                r.ProvinceId_GHN,
                r.DistrictId_GHN,
                r.WardId_GHN,
                r.PhoneNumber,
                r.Email,
                r.IsDefault
            });

            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateReceiverInformation([FromBody] List<ReceiverInformation> addresses)
        {
            var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Lấy tất cả các địa chỉ của customer này
            var existingAddresses = await _context.ReceiverInformations
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();

            foreach (var address in addresses)
            {
                // Cập nhật các địa chỉ với IsDefault
                var existingAddress = existingAddresses.FirstOrDefault(r => r.ReceiverId == address.ReceiverId);
                if (existingAddress != null)
                {
                    existingAddress.IsDefault = address.IsDefault;
                }
            }

            // Lưu các thay đổi vào database
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId.ToString());
                if (order == null)
                {
                    return NotFound("Order not found.");
                }

                order.OrderStatusId = 6; //6 là cancel order
                await _orderService.UpdateOrderOnGHNAsync(order);
                await _hubContext.Clients.All.SendAsync("NewOrder");
                return RedirectToAction("Index", "Tracking");
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmReceived(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId.ToString());
                if (order == null)
                {
                    return NotFound("Order not found.");
                }

                order.OrderStatusId = 3; //3 là confirm_received
                await _orderService.UpdateOrderOnGHNAsync(order);
                await _hubContext.Clients.All.SendAsync("NewOrder");
                return RedirectToAction("Index", "Tracking");
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
