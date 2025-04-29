using OSS_Main.Models.DTO.FilterDTO;
using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;
using OSS_Main.Service.Interface;

namespace OSS_Main.Service.Implementation
{
	public class ProductService : IProductService
	{
		private readonly IProductRepo _productRepo;

		public ProductService(IProductRepo productRepo)
		{
			_productRepo = productRepo;
		}
		public async Task<List<ProductSpec>> GetAllProductsWithSpecsAsync()
		{
			return await _productRepo.GetAllProductsWithSpecsAsync();
		}
		public async Task<List<Product>> GetAllProducts()
		{
			return await _productRepo.GetAllProducts();
		}
		public async Task<List<Category>> GetAllCategories()
		{
			return await _productRepo.GetAllCategories();
		}
		public async Task<List<Product>> GetAllProductsByCategoryId(int? categoryId)
		{
			return await _productRepo.GetAllProductsByCategoryId(categoryId);
		}
		public async Task<Product> GetAllProductsById(int? productId, int? specId)
		{
			return await _productRepo.GetAllProductsById(productId, specId);
		}
		public async Task<ProductSpec> GetProductSpecById(int? productId, int? specId)
		{
			return await _productRepo.GetProductSpecById(productId, specId);
		}

		public async Task<ProductSpec> GetProductById(int productSpecId)
		{
			return await _productRepo.GetProductById(productSpecId);
		}
		public async Task UpdateProductQuantityAfterOrder(ICollection<OrderItem> orderItem)
		{
			foreach (var item in orderItem)
			{
				if (item.CartItem.ProductSpecId != null)
				{
					var product = await _productRepo.GetProductById(item.CartItem.ProductSpecId);
					if (product.Quantity >= item.CartItem.Quantity)
					{
						product.Quantity -= item.CartItem.Quantity;
						_productRepo.UpdateProduct(product);
					}
					else
					{
						throw new Exception("Not enough stock for the product.");
					}
				}
			}
		}

		public async Task UpdateProductQuantityAfterCancel(ICollection<OrderItem> orderItem)
		{
			foreach (var item in orderItem)
			{
				if (item.CartItem.ProductSpecId != null)
				{
					var product = await _productRepo.GetProductById(item.CartItem.ProductSpecId);
					product.Quantity += item.CartItem.Quantity;
					_productRepo.UpdateProduct(product);
				}
			}
		}

		public List<ProductSpec> GetPagedProducts(int page, int pageSize, out int totalCount)
		{
			return _productRepo.GetPagedProducts(page, pageSize, out totalCount);
		}

		public bool UpdateProductWithImages(ProductSpec product, List<ProductImage> newImages)
		{
			return _productRepo.UpdateProductWithImages(product, newImages);
		}
		public void RemoveProduct(int productId)
		{
			_productRepo.RemoveProduct(productId);
		}

		public async Task<bool> AddProductWithImages(Product product, string SpecName, decimal SalePrice, decimal BasePrice, int Quantity, List<ProductImage> productImages)
		{
			return await _productRepo.AddProductWithImages(product, SpecName, SalePrice, BasePrice, Quantity, productImages);
		}

		public async Task<List<Product>> SearchProduct(FilterProductDTO filterProductDTO)
		{
			return await _productRepo.GetProductByCondition(filterProductDTO);
		}

		public async Task<long> GetTotalProduct(FilterProductDTO filterProductDTO)
		{
			return await _productRepo.GetTotalProductByCondition(filterProductDTO);
		}
	
        public  void AddReview(Feedback fb, int? orderItemId)
        {
              _productRepo.AddReview(fb, orderItemId);
        }

        public Task<List<Feedback>> GetReviews(int? productId)
        {
            return _productRepo.GetReviews(productId);
        }

		public async Task<List<Product>> SearchProductForAdmin(FilterProductDTO filterProductDTO)
		{
			return await _productRepo.GetProductByConditionForAdmin(filterProductDTO);
		}

		public async Task<long> GetTotalProductForAdmin(FilterProductDTO filterProductDTO)
		{
			return await _productRepo.GetTotalProductByConditionForAdmin(filterProductDTO);
		}

		public async Task<Product> GetProductDetailById(int productId)
		{
			return await _productRepo.GetProductDetailById(productId);
		}

		public async Task<bool> IsUpdateProductForAdmin(Product product)
		{
			return await _productRepo.IsUpdateProductForAdmin(product);
		}

		public async Task<bool> IsUpdateProductStatusById(int productId, bool status)
		{
			return await _productRepo.IsUpdateProductStatusById(productId, status);
		}

        public async Task<long> GetTotalProductTypes() => await _productRepo.GetTotalProductTypes();

		public async Task<List<ProductImage>> GetProductImagesByProductId(int productId)
		{
			return await _productRepo.GetProductImagesByProductId(productId);
		}

		public async Task<bool> IsCreateProductForAdmin(Product product)
		{
			return await _productRepo.IsCreateProductForAdmin(product);
		}
	}
}
