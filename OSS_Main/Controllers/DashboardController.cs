using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSS_Main.Service.Interface;

namespace OSS_Main.Controllers
{
    [Authorize(Roles = "Admin, Sales")]
    public class DashboardController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public DashboardController(IUserService userService, IProductService productService, IOrderService orderService)
        {
            _userService = userService;
            _productService = productService;
            _orderService = orderService;
        }
        public async Task<IActionResult> Index(int? RevenueYear)
        {
            decimal TotalCosts = await _orderService.GetTotalCostsAsync();
            long TotalAccounts = await _userService.GetTotalUsersAsync();
            long TotalProductTypes = await _productService.GetTotalProductTypes();

            ViewBag.TotalCosts = TotalCosts;
            ViewBag.TotalAccounts = TotalAccounts;
            ViewBag.TotalProductTypes = TotalProductTypes;

            int yearValue = RevenueYear ?? DateTime.Now.Year;

            List<decimal> revenueByMonth = await _orderService.GetTotalCostsByMonthAsync(yearValue);
            List<int> orderYears = await _orderService.GetAllOrderYearsAsync();
            ViewBag.OrderYears = orderYears;
            ViewBag.RevenueByMonth = "[" + string.Join(',', revenueByMonth) + "]"; //chuyển sang string để xử lý bên javascript dễ hơn
            ViewBag.RevenueYear = yearValue;
            return View();
        }
    }
}
