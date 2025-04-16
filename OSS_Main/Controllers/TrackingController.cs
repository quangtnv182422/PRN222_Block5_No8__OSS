using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSS_Main.Models.Entity;
using OSS_Main.Proxy.GHN;
using OSS_Main.Proxy.Payos;
using OSS_Main.Proxy.vnPay;
using OSS_Main.Service.Implementation;
using OSS_Main.Service.Interface;

namespace OSS_Main.Controllers
{
    [Authorize(Roles = "Customer")]
    //[Authorize]
    public class TrackingController : Controller
    {
        private readonly IVnPayProxy _vnPayService;
        private readonly IPayosProxy _payOsService;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IGhnProxy _ghnService;
        private readonly IProductService _productService;
        private readonly IReceiveInforService _receiverService;

        public TrackingController(IVnPayProxy vnPayService,
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

        public async Task<IActionResult> Index()
        {
            string userId = await _userService.GetUserIdAsync(HttpContext);
            var orders = await _orderService.GetOrderByCustomerIdAsync(userId);
            return View(orders);
        }

        public async Task<IActionResult> OrderDetail(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId.ToString());

            if (order == null)
            {
                return NotFound(); 
            }
            return View(order);
        }
    }
}
