using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSS_Main.Common;
using OSS_Main.Models.DTO.EntityDTO;
using OSS_Main.Models.DTO.FilterDTO;
using OSS_Main.Models.Entity;
using OSS_Main.Proxy.Cloundinary;
using OSS_Main.Service.Interface;
using OSS_Main.Utils;
using System.Text.Json;

namespace OSS_Main.Controllers
{
	[Authorize(Roles = "Admin, Sales")]
	public class AdminProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly ILogger<HomeController> _logger;
		private readonly ICloudinaryProxy _cloudinaryService;
		private readonly ICategoryService _categoryService;
		public AdminProductController(IProductService productService, ILogger<HomeController> logger, ICloudinaryProxy cloudinaryService, ICategoryService categoryService)
		{
			_productService = productService;
			_logger = logger;
			_cloudinaryService = cloudinaryService;
			_categoryService = categoryService;
		}
		public IActionResult ProductList()
		{
			return View();
		}

		[HttpGet]
		[Route("adminProduct/createProduct")]
		public async Task<IActionResult> CreateProductIndex()
		{
			ProductDTO product = new ProductDTO();
			SessionCommon.SetObjectAsSession(HttpContext.Session,"Product", product);
			ViewBag.Categories = await _categoryService.GetCategoriesAsync().ContinueWith(t => t.Result.Select(c => Mapper.Map<Category, CategoryDTO>(c) ?? new CategoryDTO()).ToList());
			return View("~/Views/AdminProduct/CreateProduct.cshtml", product);
		}


		[HttpGet]
		[Route("adminProduct/addSpecRow")]
		public async Task<IActionResult> AddSpecRow()
		{
			ProductDTO product = SessionCommon.GetSessionAsObject<ProductDTO>(HttpContext.Session, "Product") ?? new ProductDTO();
			product.ProductSpecs.Add(new ProductSpecDTO());
			SessionCommon.SetObjectAsSession(HttpContext.Session, "Product", product);
			ViewBag.Categories = await _categoryService.GetCategoriesAsync().ContinueWith(t => t.Result.Select(c => Mapper.Map<Category, CategoryDTO>(c) ?? new CategoryDTO()).ToList());
			return View("~/Views/AdminProduct/CreateProduct.cshtml", product);
		}
		[HttpPost]
		[Route("adminProduct/createProduct")]
		public async Task<IActionResult> CreateProduct(ProductDTO productDTO, [FromForm] List<IFormFile> productImages, [FromForm] List<int> categoryIds)
		{
			bool isValid = ModelState.IsValid;
			if (!isValid) return View("~/Views/AdminProduct/CreateProduct.cshtml", productDTO);
			foreach (var image in productImages)
			{
				var cloudinaryUrl = await _cloudinaryService.UploadImageAsync(image);
				productDTO.ProductImages.Add(new ProductImageDTO() { ImageUrl = cloudinaryUrl });
			}
			foreach(var catId in categoryIds)
			{
				productDTO.ProductCategories.Add(new ProductCategoryDTO() { CategoryId = catId });
			}
			Product product = Mapper.Map<ProductDTO, Product>(productDTO) ?? new Product();
			bool isCreated = await _productService.IsCreateProductForAdmin(product);
			if (isCreated)
			{
				TempData["SuccessMessage"] = "Product created successfully.";
				return RedirectToAction("ProductList");
			}
			else
			{
				TempData["ErrorMessage"] = "Failed to update product.";
				return View("~/Views/AdminProduct/CreateProduct.cshtml", product);
			}
		}

		[HttpGet]
		[Route("adminProduct/searchProduct")]
		public async Task<IActionResult> SearchProduct(string searchProductRequest)
		{
			FilterProductDTO filterProductDTO = JsonSerializer.Deserialize<FilterProductDTO>(searchProductRequest) ?? new FilterProductDTO();
			var products = await _productService.SearchProductForAdmin(filterProductDTO).ContinueWith(t => t.Result.Select(p => Mapper.Map<Product, ProductDTO>(p) ?? new ProductDTO()).ToList());
			var categories = await _productService.GetAllCategories().ContinueWith(t => t.Result.Select(c => Mapper.Map<Category, CategoryDTO>(c) ?? new CategoryDTO()).ToList());
			long totalProduct = await _productService.GetTotalProductForAdmin(filterProductDTO);

			int totalPage = (int)Math.Ceiling((double)totalProduct / filterProductDTO.PageSize);

			return Json(new
			{
				products = products,
				page = filterProductDTO.Page,
				totalPage = totalPage,
				isUserAuthenticated = User.Identity.IsAuthenticated && User.IsInRole("Admin"),
			});
		}


		[HttpGet]
		[Route("adminProduct/updateProduct")]
		public async Task<IActionResult> GetProductUpdateIndex(int id)
		{
			var product = await _productService.GetProductDetailById(id).ContinueWith(t => Mapper.Map<Product, ProductDTO>(t.Result) ?? new ProductDTO());
			return View("~/Views/AdminProduct/UpdateProduct.cshtml", product);
		}

		[HttpGet]
		[Route("adminProduct/getProduct")]
		public async Task<IActionResult> GetProductDetail(int id)
		{
			var product = await _productService.GetProductDetailById(id).ContinueWith(t => Mapper.Map<Product, ProductDTO>(t.Result) ?? new ProductDTO());
			return View("~/Views/AdminProduct/ProductDetail.cshtml", product);
		}

		[HttpGet]
		public async Task<IActionResult> GetProduct(int id)
		{
			try
			{
				var product = await _productService.GetProductById(id);
				if (product == null)
				{
					return Json(new { success = false, message = "Product not found" });
				}

				string categoryName = "No Category";
				if (product.Product.ProductCategories.Any())
				{
					foreach (var cat in product.Product.ProductCategories)
					{
						var category = await _categoryService.GetCategoryByIdAsync(cat.CategoryId);
						if (category != null)
						{
							categoryName = category.Name;
						}
					}


				}
				string productStatus = "Unknown";
				if (product.ProductStatus)
				{
					productStatus = "Is Published";
				}
				else
				{
					productStatus = "UnPublished";
				}

				var productDto = new
				{
					ProductSpecId = product.ProductSpecId,
					ProductName = product.SpecName ?? "N/A",
					Description = product.Product.Description ?? "N/A",
					BasePrice = product.BasePrice,
					SalePrice = product.SalePrice ?? 0,
					Quantity = product.Quantity,
					CategoryName = categoryName,
					ProductStatus = product.ProductStatus,
					ProductStatusDescription = productStatus,
					ProductImages = product.Product.ProductImages?.Select(img => img.ImageUrl).ToList() ?? new List<string>()
				};

				return Json(new { success = true, product = productDto });
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error fetching product: {ex.Message}");
				return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
			}
		}

		[HttpPost]
		public async Task<IActionResult> UpdateProduct(ProductDTO product)
		{
			bool isValid = ModelState.IsValid;
			if(!isValid) return View("~/Views/AdminProduct/UpdateProduct.cshtml", product);
			Product? product1 = Mapper.Map<ProductDTO, Product>(product);
			if (product1 != null && product.ProductImages != null)
			{
				for(int i = 0; i < product.ProductImages.Count; i++)
				{
					var image = product.ProductImages[i];
					if (image.ImageFile == null || string.IsNullOrEmpty(image.ImageUrl)) continue;
					var formFileHash = UtilHelper.ComputeSha256Hash(UtilHelper.GetBytesFromIFormFile(image.ImageFile));
					var cloudinaryHash = UtilHelper.ComputeSha256Hash(await UtilHelper.DownloadFileAsBytesAsync(image.ImageUrl));
					if (formFileHash != cloudinaryHash)
					{
						product1.ProductImages.ElementAt(i).ImageUrl = await _cloudinaryService.UploadImageAsync(image.ImageFile);
					}
				}
			}
			bool isUpdated = product1 != null ? await _productService.IsUpdateProductForAdmin(product1) : false;
			if (isUpdated)
			{
				TempData["SuccessMessage"] = "Product updated successfully.";
				return RedirectToAction("ProductList");
			}
			else
			{
				TempData["ErrorMessage"] = "Failed to update product.";
				return View("~/Views/AdminProduct/UpdateProduct.cshtml", product);
			}
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		[Route("adminProduct/deleteProduct")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			try
			{
				bool isDeleted = await _productService.IsUpdateProductStatusById(id, false);
				if (isDeleted)
				{
					TempData["DeleteMessage"] = "Product deleted successfully.";
				}
				else
				{
					TempData["DeleteMessage"] = "Product deleted failed.";
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error deleting product with ID {id}: {ex.Message}");
				return Json(new { success = false, message = "Error occurred while deleting product." });
			}
			return RedirectToAction("ProductList");
		}
		[Authorize(Roles = "Admin")]
		[HttpGet]
		[Route("adminProduct/activateProduct")]
		public async Task<IActionResult> ActivateProduct(int id)
		{
			try
			{
				bool isDeleted = await _productService.IsUpdateProductStatusById(id, true);
				if (isDeleted)
				{
					TempData["ActivateMessage"] = "Product activated successfully.";
				}
				else
				{
					TempData["ActivateMessage"] = "Product activated failed.";
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error deleting product with ID {id}: {ex.Message}");
				return Json(new { success = false, message = "Error occurred while deleting product." });
			}
			return RedirectToAction("ProductList");
		}
	}
}
