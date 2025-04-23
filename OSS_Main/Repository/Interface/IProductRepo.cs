using OSS_Main.Models.DTO.FilterDTO;
using OSS_Main.Models.Entity;

namespace OSS_Main.Repository.Interface
{
    public interface IProductRepo
    {
        Task<List<ProductSpec>> GetAllProductsWithSpecsAsync();
        Task<List<Product>> GetAllProducts();
        Task<Product> GetAllProductsById(int? productId, int? specId);
        Task<List<Category>> GetAllCategories();
        Task<List<Product>> GetAllProductsByCategoryId(int? categoryId);
        Task<ProductSpec> GetProductSpecById(int? productId, int? specId);
        Task<ProductSpec> GetProductById(int productSpecId);
        Task UpdateProduct(ProductSpec product);
        List<ProductSpec> GetPagedProducts(int page, int pageSize, out int totalCount);
        bool UpdateProductWithImages(ProductSpec product, List<ProductImage> newImages);
       void RemoveProduct(int productId);

        Task<List<Product>> GetProductByCondition(FilterProductDTO filterProductDTO);
        Task<long> GetTotalProductByCondition(FilterProductDTO filterProductDTO);
        void AddReview(Feedback feedback);
        Task<List<Feedback>> GetReviews(int? productId);
        Task<bool> AddProductWithImages(Product product, string SpecName, decimal SalePrice, decimal BasePrice, int Quantity, List<ProductImage> productImages);
    }
}
