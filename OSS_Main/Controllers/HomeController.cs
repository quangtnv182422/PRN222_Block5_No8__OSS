using Microsoft.AspNetCore.Mvc;
using OSS_Main.Models;
using OSS_Main.Models.DTO.EntityDTO;
using OSS_Main.Models.Entity;
using OSS_Main.Service.Implementation;
using OSS_Main.Service.Interface;
using OSS_Main.Utils;
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

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[Route("home/info")]
		public async Task<IActionResult> GetInformationInHomePage(int? categoryId)
		{
			var allProductsWithCategory = categoryId == null
				? await _productService.GetAllProducts()
				: await _productService.GetAllProductsByCategoryId(categoryId);

			var categories = await _productService.GetAllCategories();

			List<ProductDTO> allProductsWithCategoryDTO = allProductsWithCategory.Select(p => Mapper.Map<Product, ProductDTO>(p) ?? new ProductDTO()).ToList();
			List<CategoryDTO> categoriesDTO = categories.Select(c => Mapper.Map<Category, CategoryDTO>(c) ?? new CategoryDTO()).ToList();

			//ViewBag.SelectedCategoryId = categoryId;
			//ViewBag.AllProducts = allProductsWithCategory ?? new List<Product>();
			//ViewBag.Categories = categories;

			string userId = await _userService.GetUserIdAsync(HttpContext);
			var cart = await _cartService.GetUserCartAsync(userId);
			int cartItemCount = cart?.CartItems?.Count() ?? 0;
			HttpContext.Session.SetInt32("CartItemCount", cartItemCount);
			bool isCustomer = User.IsInRole("Customer");

			return Json(new
			{
				categories = categoriesDTO,
				products = allProductsWithCategoryDTO,
				selectedCategoryId = categoryId,
				isUserAuthenticated = User.Identity.IsAuthenticated,
				isCustomer = isCustomer
			});
		}


		[HttpGet]
		[Route("home/redirectToLoginPage")]
		public IActionResult RedirectToLoginPage()
		{
			return Redirect("/Identity/Account/Login");
		}

		[HttpGet]
		[Route("home/redirectToProductDetails")]
		public IActionResult RedirectToProductDetails(int? productId, int? specId)
		{
			return RedirectToAction("Details", "Products", new { productId = productId, specId = specId });
		}



	}
}
