using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSS_Main.Models.Entity;
using OSS_Main.Proxy.Cloundinary;
using OSS_Main.Service.Interface;

namespace OSS_Main.Controllers
{
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
        public async Task<IActionResult> ProductList(int page = 1, int pageSize = 10)
        {
            var products = _productService.GetPagedProducts(page, pageSize, out int totalCount);
            var categories = await _categoryService.GetCategoriesAsync();


            ViewBag.Categories = categories;
            foreach (var product in products)
            {
                if (product.Product.ProductImages == null)
                {
                    product.Product.ProductImages = new List<ProductImage>();
                }
               /* if (product.Product.ProductCategories == null)
                {
                    product.Product.ProductCategories = new Category { CategoryName = "No Category" };
                }*/
            }

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.CurrentPage = page;

            return View(products);
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
        public async Task<IActionResult> UpdateProduct(ProductSpec product, List<IFormFile> ProductImages)
        {

            List<ProductImage> uploadedImages = new List<ProductImage>();
            if (ProductImages != null && ProductImages.Count > 0)
            {
                foreach (var image in ProductImages)
                {
                    string imageUrl = await _cloudinaryService.UploadImageAsync(image);
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        uploadedImages.Add(new ProductImage { ImageUrl = imageUrl });
                    }
                }
            }

            bool isUpdated = _productService.UpdateProductWithImages(product, uploadedImages);
            return Json(new { success = isUpdated });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);

                if (product == null)
                {
                    return Json(new { success = false, message = "Product not found" });
                }

                if (product.Quantity > 0)
                {
                    return Json(new { success = false, message = "Product quantity must be 0 to delete" });
                }

               /* foreach (var image in product.ProductImages)
                {
                    _cloudinaryService.DeleteImage(image.ProductImageUrl);
                }*/

                //_productService.RemoveProductImages(id);

                _productService.RemoveProduct(id);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting product with ID {id}: {ex.Message}");
                return Json(new { success = false, message = "Error occurred while deleting product." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product model, string SpecName, decimal SalePrice, decimal BasePrice, int Quantity, List<IFormFile> ProductImages)
        {

            if (!ModelState.IsValid)
            {
                return Json(new { success = false });

            }

            model.CreatedAt = DateTime.Now;
            List<ProductImage> uploadedImages = new List<ProductImage>();
            if (ProductImages != null && ProductImages.Count > 0)
            {
                foreach (var image in ProductImages)
                {
                    string imageUrl = await _cloudinaryService.UploadImageAsync(image);
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        uploadedImages.Add(new ProductImage { ImageUrl = imageUrl });
                    }
                }
            }

            bool isAdded = await _productService.AddProductWithImages(model, SpecName, SalePrice, BasePrice, Quantity, uploadedImages);
            return Json(new { success = isAdded });
        }

    }
}
