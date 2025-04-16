using OSS_Main.Models.Entity;

namespace OSS_Main.Repository.Interface
{
    public interface ICartRepo
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task<Cart> CreateCartAsync(string userId);
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task RemoveCartItemAsync(CartItem cartItem);
        Task UpdateCartAsync(Cart cart);
        Task<bool> AddProductToCartAsync(string userId, int productSpecId,int quantity);
        Task<List<CartItem>> GetCartItemsByIdsAsync(List<int> cartItemIds);

    }
}
