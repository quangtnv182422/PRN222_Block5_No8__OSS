using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;
using OSS_Main.Service.Interface;

namespace OSS_Main.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICartService _cartService;
        private readonly IEmailSender _emailSender;
        private readonly IReceiveInforService _receiverService;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderService(IOrderRepo orderRepo, ICartService cartService, IEmailSender emailSender, IReceiveInforService receiverService, UserManager<AspNetUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _orderRepo = orderRepo;
            _cartService = cartService;
            _emailSender = emailSender;
            _receiverService = receiverService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Order> CreateOrderAsync(string? customerId,
                                                  string paymentMethod,
                                                  List<int> cartItemIds,
                                                  float totalCost,
                                                  int orderStatus,
                                                  string? note,
                                                  int receiverId,
                                                  string orderId_GHN)
        {
            var cartItems = await _cartService.GetCartItemsByIdsAsync(cartItemIds);

            if (cartItems == null || !cartItems.Any())
            {
                throw new InvalidOperationException("Cart items are empty or null.");
            }

            var order = new Order
            {
                OrderAt = DateTime.Now,
                CustomerId = customerId,
                ReceiverId = receiverId,
                OrderCode_GHN = orderId_GHN,
                PaymentMethod = paymentMethod,
                OrderStatusId = orderStatus,
                OrderItemOrders = cartItems.Select(ci => new OrderItem
                {
                    CartItemId = ci.CartItemId,
                    CartItem = ci
                }).ToList(),
                TotalCost = (decimal)totalCost,
                Notes = note
            };

            await _orderRepo.CreateOrderAsync(order);
            foreach (var item in cartItems)
            {
                item.CartId = null;
                await _cartService.UpdateCartItemAsync(item);
            }

            // Gửi email xác nhận (nếu cần)
            var receiverInfor = await _receiverService.GetDefaultReceiverInfoByIdAsync(receiverId);
            string fullAddress = $"{receiverInfor.Address}, {receiverInfor.WardName_GHN}, {receiverInfor.DistrictName_GHN}, {receiverInfor.ProvinceName_GHN}";
            string link = "https://localhost:7012/Products/Index";//fix cứng tạm thời
            var user = _httpContextAccessor.HttpContext?.User;
            var email = user != null ? (await _userManager.GetUserAsync(user))?.Email : null;


            await SendOrderConfirmEmail(email, receiverInfor.FullName, fullAddress, receiverInfor.PhoneNumber, note, cartItems.Select(ci => new OrderItem
            {
                CartItemId = ci.CartItemId,
                CartItem = ci
            }).ToList(), link, paymentMethod);

            return order;
        }

        public async Task SendOrderConfirmEmail(string email, string fullName, string address, string phoneNumber, string? orderNotes, List<OrderItem> products, string returnLink, string paymentMethod)
        {
            var subject = "Order Confirmation - Trái Cây Mọng 'Nước' ";

            if (products == null || !products.Any())
            {
                products = new List<OrderItem>();
            }

            orderNotes = orderNotes ?? "No special notes";
            returnLink = returnLink ?? "#";
            paymentMethod = paymentMethod ?? "Not specified";

            var productRows = "";
            foreach (var product in products)
            {
                if (product?.CartItem.ProductSpec.Product != null)
                {
                    productRows += $"\n" +
                        $"    <tr class=\"product-item\">\n" +
                        $"      <td><img src=\"{product.CartItem.ProductSpec.Product.ProductImages}\" alt=\"Đây là ảnh sách\"></td>\n" +
                        $"      <td>{product.CartItem.ProductSpec.Product.ProductName} - ({product.CartItem.ProductSpec.SpecName})</td>\n" +
                        $"      <td>{product.CartItem.ProductSpec.Product.Description}</td>\n" +
                        $"      <td>{product.CartItem.ProductSpec.SalePrice:N2} VND</td>\n" +
                        $"      <td>{(product.CartItem.ProductSpec.SalePrice * product.CartItem.Quantity):N2} VND</td>\n" +
                        $"      <td>{product.CartItem.Quantity}</td>\n" +
                        $"    </tr>";
                }
            }

            // Tạo nội dung email
            var message = "<!DOCTYPE html>\n"
                        + "<html>\n"
                        + "<head>\n"
                        + "    <style>\n"
                        + "        body { font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f4f4f4; }\n"
                        + "        .email-container { max-width: 800px; margin: 40px auto; background-color: #ffffff; padding: 20px; border: 1px solid #dddddd; border-radius: 10px; }\n"
                        + "        .header { text-align: center; padding: 10px 0; background-color: #e68d40; border-radius: 10px 10px 0 0; color: #ffffff; font-size: 24px; font-weight: bold; }\n"
                        + "        .header-icon img { height: 50px; margin: 20px 0; }\n"
                        + "        .content { padding: 20px; text-align: center; }\n"
                        + "        .content h1 { color: #333333; }\n"
                        + "        .content p { font-size: 16px; line-height: 1.5; color: #555555; }\n"
                        + "        .verify-button { display: inline-block; margin: 20px 0; padding: 15px 30px; font-size: 16px; color: #ffffff; background-color: #ce4b40; border-radius: 5px; text-decoration: none; }\n"
                        + "        .footer { text-align: center; padding: 20px; font-size: 14px; color: #aaaaaa; }\n"
                        + "        .footer a { color: #e68d40; text-decoration: none; }\n"
                        + "        .product-list { width: 100%; border-collapse: collapse; }\n"
                        + "        .product-list th, .product-list td { border: 1px solid #ddd; padding: 8px; text-align: left; }\n"
                        + "        .product-list th { background-color: #f2f2f2; }\n"
                        + "        .product-list img { max-width: 100px; height: auto; margin: auto; }\n"
                        + "        .product-item { background-color: #f9f9f9; }\n"
                        + "    </style>\n"
                        + "</head>\n"
                        + "<body>\n"
                        + "    <div class=\"email-container\">\n"
                        + "        <div class=\"header\">\n"
                        + "            FruitShop<span>.</span>\n"
                        + "            <div class=\"header-icon\"><img src=\"https://cdn-icons-png.freepik.com/512/9840/9840606.png\" alt=\"Email Icon\"></div>\n"
                        + "        </div>\n"
                        + "        <div class=\"content\">\n"
                        + "            <h1>Đơn hàng đã được ghi nhận!</h1>\n"
                        + "            <p>Hi " + fullName + ",</p>\n"
                        + "            <p>Cảm ơn bạn đã chọn mua hàng tại FruitShop, đơn hàng của bạn đã được ghi nhận .</p>\n"
                        + "            <h3>Thông tin đơn hàng</h3>\n"

                        + "<table border=\"1\">" +
                                "<tr>" +
                                    "<td><strong>Họ và tên người mua:</strong></td>" +
                                    "<td>" + fullName + "</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td><strong>Địa chỉ:</strong></td>" +
                                    "<td>" + address + "</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td><strong>Số điện thoại:</strong></td>" +
                                    "<td>" + phoneNumber + "</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td><strong>Ghi chú:</strong></td>" +
                                    "<td>" + orderNotes + "</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td><strong>Phương thức thanh toán:</strong></td>" +
                                    "<td>" + paymentMethod + "</td>" +
                                "</tr>" +
                            "</table>"

                        + "            <h2>Sản phẩm đã chọn</h2>\n"
                        + "            <table class=\"product-list\">\n"
                        + "                <thead>\n"
                        + "                    <tr><th>Ảnh</th>" +
                                                "<th>Tên sản phẩm</th>" +
                                                "<th>Mô tả </th>" +
                                                "<th>Đơn giá</th>" +
                                                "<th>Tổng giá</th>" +
                                                "<th>Số lượng</th>" +
                                                "</tr>\n"
                        + "                </thead>\n"
                        + "                <tbody>" + productRows + "</tbody>\n"
                        + "            </table>\n"
                        + "            <a href=\"" + returnLink + "\" class=\"verify-button\">Tiếp tục mua sắm</a>\n"
                        + "        </div>\n"
                        + "        <div class=\"footer\">\n"
                        + "            <p>FPT University, Hoa Lac, Ha Noi</p>\n"
                        + "            <p><a href=\"#\">Privacy Policy</a> | <a href=\"#\">Contact Details</a></p>\n"
                        + "        </div>\n"
                        + "    </div>\n"
                        + "</body>\n"
                        + "</html>";

            await _emailSender.SendEmailAsync(email, subject, message);
        }

        public async Task UpdateOrderOnGHNAsync(Order order)
        {
            await _orderRepo.UpdateOrderOnGHNAsync(order);
        }

        public async Task<Order> GetOrderByIdAsync(string orderId)
        {
            return await _orderRepo.GetOrderByIdAsync(orderId);
        }

        public async Task<List<Order>> GetOrderByCustomerIdAsync(string customerId)
        {
            return await _orderRepo.GetOrderByCustomerIdAsync(customerId);
        }
        public async Task<List<Order>> GetAllOrderAsync()
        {
            return await _orderRepo.GetAllOrderAsync();
        }

        public async Task<List<Order>> GetAllOrderShippingAsync()
        {
            return await _orderRepo.GetAllOrderShippingAsync();
        }
        public async Task<OrderStatus> GetOrderStatusByNameAsync(string orderStatusName)
        {
            return await _orderRepo.GetOrderStatusByNameAsync(orderStatusName);
        }
        public async Task<List<Order>> GetAllOrderByUserReceiverAsync(string userId)
        {
            return await _orderRepo.GetAllOrderByUserReceiverAsync(userId);
        }
        public async Task<List<OrderStatus>> GetAllOrderStatusAsync()
        {
            return await _orderRepo.GetAllOrderStatusAsync();

        }
        public async Task<decimal> GetTotalCostsAsync() => await _orderRepo.GetTotalCostsAsync();

        public async Task<List<decimal>> GetTotalCostsByMonthAsync(int year)
        {
            var result = await _orderRepo.GetTotalCostsByMonthAsync(year);

            List<decimal> revenueOfAllMonths = new List<decimal>();
            //Map doanh thu với từng tháng vì có thể có tháng doanh thu bằng 0
            for (int i = 1; i <= 12; i++)
            {
                if (result.TryGetValue(i, out decimal revenue))
                {
                    revenueOfAllMonths.Add(revenue);
                }
                else
                {
                    revenueOfAllMonths.Add(0); //tháng này doanh thu của web là 0 đồng
                }
            }
            return revenueOfAllMonths;
        }

        public async Task<List<int>> GetAllOrderYearsAsync() => await _orderRepo.GetAllOrderYearsAsync();
    }
}
