using OSS_Main.Models.Entity;

namespace OSS_Main.Service.Interface
{
    public interface IProductService
    {
        Task<List<ProductSpec>> GetAllProductsWithSpecsAsync();
        Task<List<Product>> GetAllProducts();
        Task<Product> GetAllProductsById(int? productId, int? specId);
        Task<List<Category>> GetAllCategories();
        Task<List<Product>> GetAllProductsByCategoryId(int? categoryId);
        Task<ProductSpec> GetProductSpecById(int? productId, int? specId);
        Task<ProductSpec> GetProductById(int productSpecId);
        Task UpdateProductQuantityAfterOrder(ICollection<OrderItem> orderItem);
        List<ProductSpec> GetPagedProducts(int page, int pageSize, out int totalCount);
        bool UpdateProductWithImages(ProductSpec product, List<ProductImage> newImages);
        Task UpdateProductQuantityAfterCancel(ICollection<OrderItem> orderItem);
        void RemoveProduct(int productId);
        void AddReview(Feedback fb);
        Task<List<Feedback>> GetReviews(int? productId);


        Task<bool> AddProductWithImages(Product product, string SpecName, decimal SalePrice, decimal BasePrice, int Quantity, List<ProductImage> productImages);
    }
}
