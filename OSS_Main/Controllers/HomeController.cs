using Microsoft.AspNetCore.Mvc;
using OSS_Main.Models;
using OSS_Main.Models.Entity;
using OSS_Main.Service.Implementation;
using OSS_Main.Service.Interface;
using System.Diagnostics;

namespace OSS_Main.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService, IUserService userService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
          
            var allProductsWithCategory = categoryId == null
                ? await _productService.GetAllProducts()
                : await _productService.GetAllProductsByCategoryId(categoryId);

            var categories = await _productService.GetAllCategories();

            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.AllProducts = allProductsWithCategory ?? new List<Product>();
            ViewBag.Categories = categories;

            string userId = await _userService.GetUserIdAsync(HttpContext);
            var cart = await _cartService.GetUserCartAsync(userId);
            int cartItemCount = cart?.CartItems?.Count() ?? 0;
            HttpContext.Session.SetInt32("CartItemCount", cartItemCount);

            return View();
        }



    }
}
