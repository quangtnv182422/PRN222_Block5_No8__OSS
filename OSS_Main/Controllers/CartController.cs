using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OSS_Main.Models.Entity;
using OSS_Main.Service.Interface;

namespace OSS_Main.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        // private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, /*IOrderService orderService*/ IUserService userService, IProductService productService)
        {
            _cartService = cartService;
            //_orderService = orderService;
            _userService = userService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = await _userService.GetUserIdAsync(HttpContext);
            var cart = await _cartService.GetUserCartAsync(userId);
            return View(cart);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int productSpecId, int quantity)
        {


            string userId = await _userService.GetUserIdAsync(HttpContext);

            var productSpec = await _productService.GetProductById(productSpecId);
            if (productSpec == null)
            {
                TempData["Error"] = "Sản phẩm không tồn tại!";
                return RedirectToAction("Index", "Home");
            }

            if (productSpec.Quantity < quantity)
            {
                TempData["Error"] = $"Sản phẩm hiện chỉ còn {productSpec.Quantity} sản phẩm trong kho.";
                return RedirectToAction("Details", "Products", new { productId = productSpec.ProductId, specId = productSpec.ProductSpecId });
            }


            bool result = await _cartService.AddProductToCartAsync(userId, productSpecId, quantity);
            TempData["Message"] = result ? "Sản phẩm đã được thêm vào giỏ hàng!" : "Có lỗi khi thêm sản phẩm vào giỏ hàng!";


            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public async Task<JsonResult> UpdateQuantity(int cartItemId, int quantity)
        {
            bool result = await _cartService.UpdateCartItemQuantityAsync(cartItemId, quantity);

            if (result)
            {
                // Trả về kết quả thành công
                return Json(new { success = true });
            }
            else
            {

                // Trả về kết quả thất bại
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            bool result = await _cartService.RemoveCartItemAsync(cartItemId);
            if (result)
            {
                TempData["Message"] = "Xóa sản phẩm khỏi giỏ hàng thành công.";
            }
            else
            {
                TempData["Error"] = "Không thể xóa sản phẩm khỏi giỏ hàng.";
            }
            return RedirectToAction("Index");
        }



    }
}
