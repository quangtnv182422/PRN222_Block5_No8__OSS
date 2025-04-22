using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSS_Main.Models.DTO.EntityDTO;
using OSS_Main.Models.DTO.FilterDTO;
using OSS_Main.Models.Entity;
using OSS_Main.Service.Implementation;
using OSS_Main.Service.Interface;
using OSS_Main.Utils;

namespace OSS_Main.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService service)
        {
            _productService = service;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View();
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? productId, int? specId)
        {
            if (productId == null)
            {
                return NotFound();

            }

            if (productId != null && specId == null)
            {
                TempData["Error"] = "Vui lòng chọn loại và số lượng";
            }

            var product = await _productService.GetAllProductsById(productId, specId);
            var price = await _productService.GetProductSpecById(productId, specId);

            var categories = await _productService.GetAllCategories();

            if (product == null)
            {
                return NotFound();
            }
            ViewBag.ProductDetail = product;
            ViewBag.Price = price;
            ViewBag.Categories = categories;
            ViewBag.SelectedSpecId = specId;

            return View();
        }

        [HttpGet]
        [Route("products/search")]
        public async Task<IActionResult> Search(string searchProduct)
		{
            FilterProductDTO filter = JsonSerializer.Deserialize<FilterProductDTO>(searchProduct) ?? new();
            //var allProductsWithCategory = filter.CategoryId == null
            //   ? await _productService.GetAllProducts()
            //   : await _productService.GetAllProductsByCategoryId(filter.CategoryId);

            var categories = await _productService.GetAllCategories();

            //ViewBag.SelectedCategoryId = filter.CategoryId;
            //ViewBag.AllProducts = allProductsWithCategory ?? new List<Product>();
            //ViewBag.Categories = categories;

            List<Product> products = await _productService.SearchProduct(filter);
            List<ProductDTO> productsDTO = products.Select(p => Mapper.Map<Product, ProductDTO>(p) ?? new ProductDTO()).ToList();
            long total = await _productService.GetTotalProduct(filter);
            return Json(new
            {
                products = productsDTO,
                total = total,
				categories = categories,
                isUserAuthenticated = User.Identity.IsAuthenticated,
            });
		}
    }
}
