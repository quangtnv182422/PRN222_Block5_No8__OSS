using OSS_Main.Models.DTO.FilterDTO;
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
		Task<Product> GetProductDetailById(int productId);
		Task UpdateProductQuantityAfterOrder(ICollection<OrderItem> orderItem);
        List<ProductSpec> GetPagedProducts(int page, int pageSize, out int totalCount);
        bool UpdateProductWithImages(ProductSpec product, List<ProductImage> newImages);
        Task UpdateProductQuantityAfterCancel(ICollection<OrderItem> orderItem);

        Task<bool> IsUpdateProductForAdmin(Product product);

        Task<bool> IsUpdateProductStatusById(int productId, bool status);

        Task<bool> IsCreateProductForAdmin(Product product);

        Task<List<Product>> SearchProduct(FilterProductDTO filterProductDTO);
        Task<long> GetTotalProduct(FilterProductDTO filterProductDTO);
        void RemoveProduct(int productId);
        void AddReview(Feedback fb, int? orderItemId);
        Task<List<Feedback>> GetReviews(int? productId);
		Task<List<Product>> SearchProductForAdmin(FilterProductDTO filterProductDTO);
		Task<long> GetTotalProductForAdmin(FilterProductDTO filterProductDTO);
        Task<bool> AddProductWithImages(Product product, string SpecName, decimal SalePrice, decimal BasePrice, int Quantity, List<ProductImage> productImages);
        Task<long> GetTotalProductTypes();
        Task<List<ProductImage>> GetProductImagesByProductId(int productId);
    }
}
