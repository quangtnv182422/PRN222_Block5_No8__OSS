using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using OSS_Main.Models.DTO.GHN;
using OSS_Main.Models.DTO.Vnpay;
using OSS_Main.Models.Entity;
using OSS_Main.Proxy.GHN;
using OSS_Main.Proxy.Payos;
using OSS_Main.Proxy.vnPay;
using OSS_Main.Service.Implementation;
using OSS_Main.Service.Interface;
using System.Text.Json;

namespace OSS_Main.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        //	9704198526191432198
        // NGUYEN VAN A
        // 07/15
        private readonly IVnPayProxy _vnPayService;
        private readonly IPayosProxy _payOsService;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IGhnProxy _ghnService;
        private readonly IProductService _productService;
        private readonly IReceiveInforService _receiverService;



        public PaymentController(IVnPayProxy vnPayService,
                                IOrderService orderService,
                                IProductService productService,
                                IPayosProxy payOsService,
                                IConfiguration configuration,
                                IUserService userService,
                                IGhnProxy ghnService,
                                IReceiveInforService receiverService
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
        }

        /// <summary>
		/// Xử lý thanh toán từ form checkout
		/// </summary>
		[HttpPost]
        public async Task<IActionResult> ProcessPayment(string paymentMethod,
                                                        string selectedItems,
                                                        decimal totalCost,
                                                        string deliveryNotes,
                                                        int receiverInforId)
        {
            //Lấy từng sản phẩm trong giỏ hàng 
            var cartItemIds = selectedItems.Split(",").Select(int.Parse).ToList();

            //Lấy Id user nếu có đăng nhập
            var userId = await _userService.GetCurrentUserIdAsync();

            var receiverInfor = await _receiverService.GetDefaultReceiverInfoByIdAsync(receiverInforId);
            string fullAddress = $"{receiverInfor.Address}, {receiverInfor.WardName_GHN}, {receiverInfor.DistrictName_GHN}, {receiverInfor.ProvinceName_GHN}";

            //Tạo order lưu vào DB với trạng thái pending
            var order = await _orderService.CreateOrderAsync(userId, paymentMethod, cartItemIds,
                                                (float)totalCost, 1, deliveryNotes, receiverInforId, null); // 1 là pending confirm dành cho COD

            // DTO để lưu thông tin order được gửi qua GHN
            var shippingOrder = new ShippingOrder
            {
                shop_id = _configuration["GHNSettings:ShopId"],
                payment_type_id = 2,
                from_phone = _configuration["GHNSettings:SenderPhone"],
                from_address = _configuration["GHNSettings:FromAddress"],
                from_ward_name = _configuration["GHNSettings:FromWardName"],
                from_district_name = _configuration["GHNSettings:FromDistrictName"],
                from_provice_name = _configuration["GHNSettings:FromProvinceName"],
                note = deliveryNotes,
                required_note = "KHONGCHOXEMHANG",
                to_name = receiverInfor.FullName,
                to_phone = receiverInfor.PhoneNumber,
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
                                    quantity = cartItemIds.Count,
                                    weight = 1200,
                                    category = new ShippingOrder.ItemOrderGHN.CategoryGHN { level1 = "Sách" }
                                }
                            }
            };
            // Lưu thông tin shippingOrder vào TempData
            TempData["ShippingOrder"] = JsonSerializer.Serialize(shippingOrder);


            switch (paymentMethod)
            {
                case "COD":
                    //COD thì chưa vội gửi order shipping sang cho GHN
                   
                  /*  var shippingResponse = await _ghnService.SendShippingOrderAsync(shippingOrder);
                    if (shippingResponse.Contains("Error"))
                    {
                        return BadRequest("Failed to create shipping order: " + shippingResponse);
                    }
                    else
                    {
                        //Gán OrderId củ GHN vào DB
                        var json = JsonDocument.Parse(shippingResponse);
                        string orderCode = json.RootElement.GetProperty("data").GetProperty("order_code").GetString();
                        order.OrderCode_GHN = orderCode;
                        await _orderService.UpdateOrderOnGHNAsync(order);
                    }*/


                    return RedirectToAction("PaymentSuccess");

                case "vnPay":

                    var paymentModel = new PaymentInformationModel
                    {
                        OrderType = "other",
                        Amount = (double)totalCost,
                        OrderDescription = "Thanh toán bằng vnPay tại sneaker Shoes",
                        Name = receiverInfor.FullName,
                        OrderId = order.OrderId.ToString(),
                    };

                    return RedirectToAction("CreatePaymentUrlVnpay", "Payment", paymentModel);

                case "PayOS":
                    HttpContext.Session.SetString("OrderId", order.OrderId.ToString());
                    var paymentLinkRequest = new PaymentData(
                                              orderCode: int.Parse(DateTimeOffset.Now.ToString("ffffff")),
                                              amount: (int)totalCost,
                                              description: "Thanh toan ma QR",
                                              items: [new("Item test", 1, 2000)],
                                              returnUrl: _configuration["PayOS:PayOSReturnUrl"],
                                              cancelUrl: _configuration["PayOS:PayOSReturnUrl"]);

                    return RedirectToAction("CreatePaymentPayOS", "Payment", paymentLinkRequest);

                default:
                    return RedirectToAction("Checkout", "Cart");
            }
        }

        /// <summary>
        /// Xử lý thanh toán từ form payment again
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ProcessPaymentAgain( int orderId,
                                                        string paymentMethod,
                                                        int receiverInforId)
        {

            var receiverInfor = await _receiverService.GetDefaultReceiverInfoByIdAsync(receiverInforId);
            string fullAddress = $"{receiverInfor.Address}, {receiverInfor.WardName_GHN}, {receiverInfor.DistrictName_GHN}, {receiverInfor.ProvinceName_GHN}";

            //Tìm theo OrderId
            var order = await _orderService.GetOrderByIdAsync(orderId.ToString()); 

            // DTO để lưu thông tin order được gửi qua GHN
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
                to_name = receiverInfor.FullName,
                to_phone = receiverInfor.PhoneNumber,
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
                                    quantity = order.OrderItemOrders.Count(),
                                    weight = 1200,
                                    category = new ShippingOrder.ItemOrderGHN.CategoryGHN { level1 = "Quả" }
                                }
                            }
            };
            // Lưu thông tin shippingOrder vào TempData
            TempData["ShippingOrder"] = JsonSerializer.Serialize(shippingOrder);


            switch (paymentMethod)
            {
                case "vnPay":

                    var paymentModel = new PaymentInformationModel
                    {
                        OrderType = "other",
                        Amount = (double)order.TotalCost,
                        OrderDescription = "Thanh toán bằng vnPay tại sneaker Shoes",
                        Name = receiverInfor.FullName,
                        OrderId = order.OrderId.ToString(),
                    };

                    return RedirectToAction("CreatePaymentUrlVnpay", "Payment", paymentModel);

                case "PayOS":
                    HttpContext.Session.SetString("OrderId", order.OrderId.ToString());
                    var paymentLinkRequest = new PaymentData(
                                              orderCode: int.Parse(DateTimeOffset.Now.ToString("ffffff")),
                                              amount: (int)order.TotalCost,
                                              description: "Thanh toan ma QR",
                                              items: [new("Item test", 1, 2000)],
                                              returnUrl: _configuration["PayOS:PayOSReturnUrl"],
                                              cancelUrl: _configuration["PayOS:PayOSReturnUrl"]);

                    return RedirectToAction("CreatePaymentPayOS", "Payment", paymentLinkRequest);

                default:
                    return RedirectToAction("Checkout", "Cart");
            }
        }

        /// <summary>
        /// Trả về trang thanh toán thất bại
        /// </summary>
        public IActionResult PaymentFail()
        {
            return View();
        }
        /// <summary>
        /// Trả về trang thanh toán thành công
        /// </summary>
        public IActionResult PaymentSuccess()
        {
            return View();
        }


        /// <summary>
		/// Tạo URL thanh toán VNPay và chuyển hướng đến VNPay
		/// </summary>
		public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Redirect(url);
        }



        /// <summary>
        /// Xử lý phản hồi từ VNPay sau khi thanh toán
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> PaymentCallbackVnpay()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }

            TempData["Message"] = $"Thanh toán thành công: {response.VnPayResponseCode}";
            //Đổi order status sang 2
            if (int.TryParse(response.OrderId, out int orderId))
            {
                var updateOrder = await _orderService.GetOrderByIdAsync(response.OrderId);
                if (updateOrder != null)
                {
                    updateOrder.OrderStatusId = 2;
                    updateOrder.PaymentMethod = "vnPay";
                    await _orderService.UpdateOrderOnGHNAsync(updateOrder);

                    await _productService.UpdateProductQuantityAfterOrder(updateOrder.OrderItemOrders); // trừ sản phẩm sau khi order trong product

                    // Lấy thông tin shippingOrder từ TempData
                    var shippingOrder = JsonSerializer.Deserialize<ShippingOrder>(TempData["ShippingOrder"] as string);

                    //Gửi thông tin Order cho GHN (Tạm tắt vì GHN giới hạn test cho 3 đơn)

                    if (shippingOrder != null)
                    {
                        // Gửi thông tin đơn hàng cho GHN sau khi thanh toán thành công
                        var shippingResponse = await _ghnService.SendShippingOrderAsync(shippingOrder);
                        if (shippingResponse.Contains("Error"))
                        {
                            return BadRequest("Failed to create shipping order: " + shippingResponse);
                        }
                        else
                        {
                            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(shippingResponse);
                            string orderCode = jsonResponse.GetProperty("data").GetProperty("order_code").GetString();
                            updateOrder.OrderCode_GHN = orderCode;
                            await _orderService.UpdateOrderOnGHNAsync(updateOrder);

                        }

                    }

                }
            }
            else
            {
                TempData["Message"] = "Lỗi xác nhận đơn hàng: Không thể chuyển đổi OrderId.";
            }
            return RedirectToAction("PaymentSuccess");
        }


        /// <summary>
        /// Xử lý thanh toán qua PayOS
        /// </summary>
        public async Task<IActionResult> CreatePaymentPayOS(PaymentData data)
        {
            var url = await _payOsService.CreatePayOSPaymentUrl(data);
            return Redirect(url.checkoutUrl);
        }


        /// <summary>
        /// Xử lý thông tin thanh toán trả về từ PayOS
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> PaymentCallbackPayOS()
        {
            var response = _payOsService.ProcessReturnUrl(Request.Query);

            if (response == null || response.Code != "00")
            {
                return RedirectToAction("PaymentFail");
            }
            else if (response.Status == "PAID")
            {
                var orderId = HttpContext.Session.GetString("OrderId");
                var updateOrder = await _orderService.GetOrderByIdAsync(orderId);
                if (updateOrder != null)
                {
                    updateOrder.OrderStatusId = 2;
                    updateOrder.PaymentMethod = "PayOS";
                    await _orderService.UpdateOrderOnGHNAsync(updateOrder);

                    await _productService.UpdateProductQuantityAfterOrder(updateOrder.OrderItemOrders); // trừ sản phẩm sau khi order trong product

                    // Lấy thông tin shippingOrder từ TempData
                    var shippingOrder = JsonSerializer.Deserialize<ShippingOrder>(TempData["ShippingOrder"] as string);

                    if (shippingOrder != null)
                    {
                        // Gửi thông tin đơn hàng cho GHN sau khi thanh toán thành công
                        var shippingResponse = await _ghnService.SendShippingOrderAsync(shippingOrder);
                        if (shippingResponse.Contains("Error"))
                        {
                            return BadRequest("Failed to create shipping order: " + shippingResponse);
                        }
                        else
                        {
                            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(shippingResponse);
                            string orderCode = jsonResponse.GetProperty("data").GetProperty("order_code").GetString();
                            updateOrder.OrderCode_GHN = orderCode;
                            await _orderService.UpdateOrderOnGHNAsync(updateOrder);

                        }

                    }

                }
                return RedirectToAction("PaymentSuccess");

            }
            return RedirectToAction("PaymentFail");

        }
    }
}
