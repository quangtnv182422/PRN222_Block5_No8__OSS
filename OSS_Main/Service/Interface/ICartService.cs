using OSS_Main.Models.Entity;

namespace OSS_Main.Service.Interface
{
    public interface ICartService
    {
        Task<Cart> GetUserCartAsync(string userId);
        Task<bool> UpdateCartItemQuantityAsync(int cartItemId, int quantity);
        Task<bool> RemoveCartItemAsync(int cartItemId);
        Task UpdateCartAsync(Cart cart);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task<bool> AddProductToCartAsync(string userId, int productId, int quantity);
        Task<List<CartItem>> GetCartItemsByIdsAsync(List<int> cartItemIds);
        Task<Cart> CreateCartAsync(string userId);
    }
}
