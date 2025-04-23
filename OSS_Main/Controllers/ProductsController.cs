using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSS_Main.Models.Entity;
using OSS_Main.Service.Implementation;
using OSS_Main.Service.Interface;

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
        public async Task<IActionResult> Index(int? categoryId)
        {
            var allProductsWithCategory = categoryId == null
               ? await _productService.GetAllProducts()
               : await _productService.GetAllProductsByCategoryId(categoryId);

            var categories = await _productService.GetAllCategories();

            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.AllProducts = allProductsWithCategory ?? new List<Product>();
            ViewBag.Categories = categories;


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
    }
}
