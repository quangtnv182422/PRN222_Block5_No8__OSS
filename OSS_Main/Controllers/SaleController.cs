using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OSS_Main.Hubs;
using OSS_Main.Models.DTO.GHN;
using OSS_Main.Proxy.GHN;
using OSS_Main.Proxy.Payos;
using OSS_Main.Proxy.vnPay;
using OSS_Main.Service.Interface;
using System.Text.Json;

namespace OSS_Main.Controllers
{
    [Authorize(Roles = "Admin, Sales")]
    public class SaleController : Controller
    {
        private readonly IVnPayProxy _vnPayService;
        private readonly IPayosProxy _payOsService;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IGhnProxy _ghnService;
        private readonly IProductService _productService;
        private readonly IReceiveInforService _receiverService;
        private readonly IHubContext<CustomerHub> _hubContext;
        public SaleController(IVnPayProxy vnPayService,
                            IOrderService orderService,
                            IProductService productService,
                            IPayosProxy payOsService,
                            IConfiguration configuration,
                            IUserService userService,
                            IGhnProxy ghnService,
                            IReceiveInforService receiverService,
                            IHubContext<CustomerHub> hubContext
                                      )
        {
            _vnPayService = vnPayService;
            _payOsService = payOsService;
            _orderService = orderService;
            _productService = productService;
            _configuration = configuration;
            _userService = userService;
            _ghnService = ghnService;
            _receiverService = receiverService;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrderAsync();
            return View(orders);
        }
        public async Task<IActionResult> PrintOrder(List<string> orderCode_GHN)
        {
            try
            {
                OrderRequest order = new OrderRequest { order_codes = orderCode_GHN };
                if (order == null)
                {
                    return NotFound("Order not found.");
                }

                var printResult = await _ghnService.PrintOrderAsync(order);


                return Content(printResult, "text/html");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        public async Task<IActionResult> ConfirmOrder(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId.ToString());
                if (order == null)
                {
                    return NotFound("Order not found.");
                }



                var receiverInfor = await _receiverService.GetDefaultReceiverInfoByIdAsync(order.ReceiverId);
                string fullAddress = $"{receiverInfor.Address}, {receiverInfor.WardName_GHN}, {receiverInfor.DistrictName_GHN}, {receiverInfor.ProvinceName_GHN}";
                var shippingOrder = new ShippingOrder
                {
                    shop_id = _configuration["GHNSettings:ShopId"],
                    payment_type_id = 2,
                    from_phone = _configuration["GHNSettings:SenderPhone"],
                    from_address = _configuration["GHNSettings:FromAddress"],
                    from_ward_name = _configuration["GHNSettings:FromWardName"],
                    from_district_name = _configuration["GHNSettings:FromDistrictName"],
                    from_provice_name = _configuration["GHNSettings:FromProvinceName"],
                    note = order.Notes,
                    required_note = "KHONGCHOXEMHANG",
                    to_name = order.Receiver.FullName,
                    to_phone = order.Receiver.PhoneNumber,
                    to_address = fullAddress,
                    to_ward_code = receiverInfor.WardId_GHN,
                    to_district_id = int.TryParse(receiverInfor.DistrictId_GHN, out int districtId) ? districtId : 0,
                    cod_amount = 20000,//max tối đa COD của GHN là 300k
                    weight = 200,// fix cứng vì ko có lưu weight, lenght, width, height
                    length = 20,
                    width = 15,
                    height = 5,
                    service_type_id = 2,
                    items = new List<ShippingOrder.ItemOrderGHN>
                            {
                                new ShippingOrder.ItemOrderGHN
                                {
                                    name = "Hoa Quả",
                                    quantity = order.OrderItemOrders.Count,
                                    weight = 1200,
                                    category = new ShippingOrder.ItemOrderGHN.CategoryGHN { level1 = "Sách" }
                                }
                            }
                };

                var shippingResponse = await _ghnService.SendShippingOrderAsync(shippingOrder);
                if (shippingResponse.Contains("Error"))
                {
                    return BadRequest("Failed to create shipping order: " + shippingResponse);
                }
                else
                {
                    var json = JsonDocument.Parse(shippingResponse);
                    string orderCode = json.RootElement.GetProperty("data").GetProperty("order_code").GetString();
                    order.OrderCode_GHN = orderCode;
                    order.OrderStatusId = 2;
                    await _orderService.UpdateOrderOnGHNAsync(order);
                    await _productService.UpdateProductQuantityAfterOrder(order.OrderItemOrders);//Trừ số lượng
                }

                await _hubContext.Clients.All.SendAsync("ConfirmOrder", "confirm", order);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        public async Task<IActionResult> CancelOrderGHN(int orderId, string orderId_GHN)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId.ToString());
                if (order == null)
                {
                    return NotFound("Order not found.");
                }



                var cancelResponse = await _ghnService.CancelOrderOnGhnAsync(orderId_GHN);

                if (!cancelResponse)
                {
                    return BadRequest("Failed to create shipping order");
                }
                else
                {
                    order.OrderStatusId = 6; //6 là cancel order
                    await _orderService.UpdateOrderOnGHNAsync(order);
                    await _productService.UpdateProductQuantityAfterCancel(order.OrderItemOrders);//Trả lại số lượng
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        public async Task<IActionResult> CancelOrderAfterConfirm(int orderId)
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
                await _productService.UpdateProductQuantityAfterCancel(order.OrderItemOrders);//Trả lại số lượng
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        public async Task<IActionResult> ConfirmReturned(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId.ToString());
                if (order == null)
                {
                    return NotFound("Order not found.");
                }
                order.OrderStatusId = 26; //26 là confirm_returned 
                await _orderService.UpdateOrderOnGHNAsync(order);
                await _productService.UpdateProductQuantityAfterCancel(order.OrderItemOrders);//Trả lại số lượng
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
