using Microsoft.EntityFrameworkCore;
using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;

namespace OSS_Main.Repository.Implementation
{
    public class CartRepo : ICartRepo
    {
        private readonly Prn222ProjectContext _context;
        public CartRepo(Prn222ProjectContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.ProductSpec)
                .ThenInclude(ci => ci.Product)
                .ThenInclude(ci => ci.ProductImages)
                .FirstOrDefaultAsync(c => c.CustomerId == userId);
        }

        public async Task<Cart> CreateCartAsync(string userId)
        {
            var cart = new Cart { CustomerId = userId, CartItems = new List<CartItem>() };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }


        public async Task<List<CartItem>> GetCartItemsByIdsAsync(List<int> cartItemIds)
        {
            return await _context.CartItems
       .Include(ci => ci.Cart)
       .Include(ci => ci.ProductSpec)
       .ThenInclude(ps => ps.Product)
       .ThenInclude(p => p.ProductImages)
       .Where(ci => cartItemIds.Contains(ci.CartItemId)) 
       .ToListAsync();
        }



        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await _context.CartItems
                .Include(c => c.ProductSpec)                
                .FirstOrDefaultAsync(c => c.CartItemId == cartItemId);
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _context.Update(cartItem);
            var cart = await _context.Carts
                            .Include(c => c.CartItems)
                            .FirstOrDefaultAsync(c => c.CartItems.Any(ci => ci.CartItemId == cartItem.CartItemId));

            if (cart != null)
            {
                cart.TotalPrice = cart.CartItems.Sum(i => i.PriceEachItem * i.Quantity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);

            var cart = await _context.Carts
                                     .Include(c => c.CartItems)  // Đảm bảo rằng chúng ta bao gồm CartItems trong Cart
                                     .FirstOrDefaultAsync(c => c.CartItems.Any(ci => ci.CartItemId == cartItem.CartItemId));

            if (cart != null)
            {
                cart.TotalPrice = cart.CartItems.Sum(i => i.PriceEachItem * i.Quantity);
            }

            await _context.SaveChangesAsync();
        }



        public async Task UpdateCartAsync(Cart cart)
        {


            _context.Update(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AddProductToCartAsync(string userId, int productSpecId, int quantity)
        {
            var cart = await _context.Carts.Include(c => c.CartItems)
                                           .FirstOrDefaultAsync(c => c.CustomerId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerId = userId,
                    TotalPrice = 0
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductSpecId == productSpecId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.PriceEachItem = existingItem.Quantity * _context.ProductSpecs.First(p => p.ProductSpecId == productSpecId).SalePrice ?? 0;
            }
            else
            {
                var spec = await _context.ProductSpecs.FirstOrDefaultAsync(p => p.ProductSpecId == productSpecId);
                if (spec != null)
                {
                    var newItem = new CartItem
                    {
                        CartId = cart.CartId,
                        ProductSpecId = productSpecId,
                        Quantity = quantity,
                        PriceEachItem = (spec.SalePrice ?? 0) * quantity,
                        ProductName = spec.Product.ProductName,
                        Description = spec.Product.Description,
                        SpecName = spec.SpecName,
                        BasePrice = spec.BasePrice,
                        SalePrice = spec.SalePrice,
                    };
                    _context.CartItems.Add(newItem);
                }
            }

            cart.TotalPrice = cart.CartItems.Sum(i => i.PriceEachItem * i.Quantity);


            await _context.SaveChangesAsync();
            return true;
        }
    }
}
