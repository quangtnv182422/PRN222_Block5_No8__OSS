using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;
using OSS_Main.Service.Interface;

namespace OSS_Main.Service.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartService(ICartRepo cartRepository, IHttpContextAccessor httpContextAccessor)
        {
            _cartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Cart> GetUserCartAsync(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await _cartRepository.GetCartByUserIdAsync(userId);
            }
            return null;
        }
        public async Task<Cart> CreateCartAsync(string userId)
        {
            return await _cartRepository.CreateCartAsync(userId);
        }

        public async Task<bool> UpdateCartItemQuantityAsync(int cartItemId, int quantity)
        {
            var cartItem = await _cartRepository.GetCartItemByIdAsync(cartItemId);


            if (cartItem == null)
            {
                return false;
            }


            if (quantity <= 0)
            {
                await _cartRepository.RemoveCartItemAsync(cartItem);
            }
            else if (quantity > cartItem.ProductSpec.Quantity)
            {
                return false;
            }
            else
            {
                cartItem.Quantity = quantity;
                await _cartRepository.UpdateCartItemAsync(cartItem);
            }
            return true;
        }

        public async Task<bool> RemoveCartItemAsync(int cartItemId)
        {

            var cartItem = await _cartRepository.GetCartItemByIdAsync(cartItemId);
            if (cartItem != null)
            {
                await _cartRepository.RemoveCartItemAsync(cartItem);
                return true;
            }
            return false;
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            await _cartRepository.UpdateCartAsync(cart);
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            await _cartRepository.UpdateCartItemAsync(cartItem);
        }
        public async Task<bool> AddProductToCartAsync(string userId, int productId, int quantity)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await _cartRepository.AddProductToCartAsync(userId, productId, quantity);
            }

            return false;
        }

        public async Task<List<CartItem>> GetCartItemsByIdsAsync(List<int> cartItemIds)
        {
            return await _cartRepository.GetCartItemsByIdsAsync(cartItemIds); ;
        }
    }
}