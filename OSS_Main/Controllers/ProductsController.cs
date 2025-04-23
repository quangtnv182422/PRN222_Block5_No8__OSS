using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSS_Main.Models.Entity;
using OSS_Main.Proxy.Cloundinary;
using OSS_Main.Service.Implementation;
using OSS_Main.Service.Interface;

namespace OSS_Main.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICloudinaryProxy _cloudinaryService;
        public ProductsController(IProductService service, ICloudinaryProxy cloudinaryProxy)
        {
            _productService = service;
            _cloudinaryService= cloudinaryProxy;
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
        [HttpPost]
        public async Task<IActionResult> AddFeedback(IFormFile? ImageFile, IFormFile? VideoFile, int Rating, string Comment, int ProductId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            var feedback = new Feedback
            {
                CustomerId = userId,
                FeedbackContent = Comment,
                RatedStar = Rating,
                CreatedAt = DateTime.Now,
                ProductId = ProductId,
                Status = "Active" // hoặc "Active", tùy theo quy ước
            };

            var mediaList = new List<Media>();

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(ImageFile);
                mediaList.Add(new Media
                {
                    Url = imageUrl,
                    PublicId = "", // Nếu Cloudinary trả về publicId thì lưu ở đây
                    MediaType = MediaType.Image,
                    CreatedAt = DateTime.Now
                });
            }

            if (VideoFile != null && VideoFile.Length > 0)
            {
                var videoUrl = await _cloudinaryService.UploadImageAsync(VideoFile);
                mediaList.Add(new Media
                {
                    Url = videoUrl,
                    PublicId = "", // Tương tự
                    MediaType = MediaType.Video,
                    CreatedAt = DateTime.Now
                });
            }

            feedback.Medias = mediaList;

             _productService.AddReview(feedback);

            return RedirectToAction("Details", new { productId = ProductId });
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
            var feedback = await _productService.GetReviews(productId);
            var paged = feedback.Skip((1 - 1) * 2).Take(2).ToList();
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.ProductDetail = product;
            ViewBag.Price = price;
            ViewBag.Categories = categories;
            ViewBag.SelectedSpecId = specId;
            ViewBag.FeedBacks = paged;
            // Các ViewBag dùng cho _FeedbackList
            ViewBag.TotalPages = (int)Math.Ceiling((double)feedback.Count / 2);
            ViewBag.CurrentPage = 1;
            ViewBag.Sort = "newest";
            ViewBag.ProductId = productId;

            ViewBag.FeedBacks = paged;

            return View();
        }
        public async Task<IActionResult> GetSortedFeedback(int? productId, string sort, int page = 1)
        {
            int pageSize = 2; // số feedback mỗi trang

            var feedbacks = await _productService.GetReviews(productId) ?? new List<Feedback>();

            feedbacks = sort switch
            {
                "oldest" => feedbacks.OrderBy(f => f.CreatedAt).ToList(),
                "rating_desc" => feedbacks.OrderByDescending(f => f.RatedStar).ToList(),
                "rating_asc" => feedbacks.OrderBy(f => f.RatedStar).ToList(),
                _ => feedbacks.OrderByDescending(f => f.CreatedAt).ToList(),
            };

            var paged = feedbacks.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.TotalPages = (int)Math.Ceiling((double)feedbacks.Count / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.Sort = sort;
            ViewBag.ProductId = productId;

            return PartialView("_FeedbackList", paged);
        }


    }
}
