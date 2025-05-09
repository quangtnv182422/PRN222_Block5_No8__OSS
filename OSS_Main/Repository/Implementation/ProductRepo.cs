﻿using Microsoft.EntityFrameworkCore;
using OSS_Main.Models.DTO.FilterDTO;
using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;

namespace OSS_Main.Repository.Implementation
{
	public class ProductRepo : IProductRepo
	{
		private readonly Prn222ProjectContext _context;
		public ProductRepo(Prn222ProjectContext context)
		{
			_context = context;
		}
		public async Task<List<ProductSpec>> GetAllProductsWithSpecsAsync()
		{
			return await _context.ProductSpecs
				.Include(p => p.Product)
				.Where(p => p.ProductStatus == true)
				.OrderByDescending(p => p.Product.CreatedAt)
				.ToListAsync();
		}

		public async Task<List<Product>> GetAllProducts()
		{
			return await _context.Products
				.Include(p => p.ProductImages)
				//.Include(p => p.ProductCategories)
				.Include(p => p.ProductSpecs)
				.Where(p => p.ProductStatus == true)
				.OrderByDescending(p => p.CreatedAt)
				.ToListAsync();
		}

		public async Task UpdateProduct(ProductSpec product)
		{
			_context.ProductSpecs.Update(product);
			await _context.SaveChangesAsync();
		}

		public async Task<Product> GetAllProductsById(int? productId, int? specId)
		{
			if (specId != null)
			{
				return await _context.Products
			   .Include(p => p.ProductImages)
			   //.Include(p => p.ProductCategories)
			   .Include(p => p.ProductSpecs)
			   .Where(p => p.ProductStatus == true)
			   .Where(p => p.ProductId == productId)
			   .Where(p => p.ProductSpecs.Any(pc => pc.ProductSpecId == specId))
			   .FirstOrDefaultAsync();
			}
			else
			{
				return await _context.Products
								.Include(p => p.ProductImages)
								//.Include(p => p.ProductCategories)
								.Include(p => p.ProductSpecs)
								.Where(p => p.ProductStatus == true)
								.Where(p => p.ProductId == productId)
								.FirstOrDefaultAsync();
			}
		}


		public async Task<List<Product>> GetAllProductsByCategoryId(int? categoryId)
		{
			if (categoryId == null)
			{
				return new List<Product>();
			}

			return await _context.Products
				.Include(p => p.ProductImages)
				// .Include(p => p.ProductCategories)
				.Include(p => p.ProductSpecs)
				.Where(p => p.ProductStatus == true && p.ProductCategories.Any(pc => pc.CategoryId == categoryId))
				.OrderByDescending(p => p.CreatedAt)
				.ToListAsync();
		}


		public async Task<List<Category>> GetAllCategories()
		{
			return await _context.Categories
				.ToListAsync();
		}

		public async Task<ProductSpec> GetProductSpecById(int? productId, int? specId)
		{
			if (specId != null)
			{
				return await _context.ProductSpecs
						   .Include(p => p.Product)
						   .Where(p => p.ProductStatus == true)
						   .Where(p => p.ProductId == productId)
						   .Where(p => p.ProductSpecId == specId)
						   .FirstOrDefaultAsync();
			}
			else
			{
				return await _context.ProductSpecs
						  .Include(p => p.Product)
						  .Where(p => p.ProductStatus == true)
						  .Where(p => p.ProductId == productId)
						  .FirstOrDefaultAsync();
			}
		}

		public async Task<ProductSpec> GetProductById(int productSpecId)
		{
			return await _context.ProductSpecs
				.Include(p => p.Product)
				.Include(p => p.Product.ProductImages)
				 .FirstOrDefaultAsync(p => p.ProductSpecId == productSpecId);
		}

		public List<ProductSpec> GetPagedProducts(int page, int pageSize, out int totalCount)
		{
			totalCount = _context.ProductSpecs.Count();
			return _context.ProductSpecs
				.Include(p => p.Product)
						   .ThenInclude(p => p.ProductImages)
						   .Skip((page - 1) * pageSize)
						   .Take(pageSize)
						   .ToList();
		}

		public bool UpdateProductWithImages(ProductSpec product, List<ProductImage> newImages)
		{
			try
			{
				var existingProduct = _context.ProductSpecs
									.Include(p => p.Product)
									.ThenInclude(p => p.ProductImages)
									.FirstOrDefault(p => p.ProductSpecId == product.ProductSpecId);

				if (existingProduct == null)
					return false;

				existingProduct.SpecName = product.SpecName;
				existingProduct.BasePrice = product.BasePrice;
				existingProduct.SalePrice = product.SalePrice;
				existingProduct.Quantity = product.Quantity;
				existingProduct.ProductStatus = product.ProductStatus;

				if (newImages != null && newImages.Count > 0)
				{
					_context.ProductImages.RemoveRange(existingProduct.Product.ProductImages);
					existingProduct.Product.ProductImages = newImages;
				}

				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error updating product: " + ex.Message);
				return false;
			}
		}

		public void RemoveProduct(int productId)
		{
			var product = _context.ProductSpecs.FirstOrDefault(p => p.ProductSpecId == productId);
			if (product != null)
			{
				_context.ProductSpecs.Remove(product);
				_context.SaveChanges();
			}
		}

		public async Task<Product> AddNewProduct(Product product)
		{
			product.ProductStatus = true;
			var addedProduct = _context.Products.Add(product);
			await _context.SaveChangesAsync();
			return addedProduct.Entity;
		}


		public async Task<bool> AddProductWithImages(Product product, string SpecName, decimal SalePrice, decimal BasePrice, int Quantity, List<ProductImage> productImages)
		{
			var newProduct = await AddNewProduct(product);

			ProductSpec spec = new ProductSpec
			{
				SpecName = SpecName,
				Quantity = Quantity,
				BasePrice = BasePrice,
				SalePrice = SalePrice,
				ProductStatus = true,
				ProductId = newProduct.ProductId
			};

			newProduct.ProductSpecs.Add(spec);

			foreach (var image in productImages)
			{
				image.ProductId = newProduct.ProductId; // Gán ProductId cho mỗi image
				_context.ProductImages.Add(image); // Thêm hình ảnh vào DbContext
			}

            // Lưu tất cả thay đổi vào cơ sở dữ liệu
            return await _context.SaveChangesAsync() > 0;
        }
        public IEnumerable<Feedback> GetReviewsByProduct(int productId)
        {
            return _context.Feedbacks
                .Where(f => f.ProductId == productId && f.Status == "Enable" )
                .OrderByDescending(f => f.RatedStar)
                .ThenByDescending(f => f.CreatedAt)
                .ToList();
        }

		public async Task<List<Product>> GetProductByCondition(FilterProductDTO filterProductDTO)
		{
			var query = _context.Products
				.Include(p => p.ProductImages)
				.Include(p => p.ProductSpecs).AsQueryable();
			if (filterProductDTO.CategoryId != 0)
			{
				query = query.Where(p => p.ProductCategories.Any(pc => pc.CategoryId == filterProductDTO.CategoryId));
			}
			if (!string.IsNullOrEmpty(filterProductDTO.SortingCategory))
			{
				switch (filterProductDTO.SortingCategory)
				{
					case "price":
						query = query.OrderBy(p => p.ProductSpecs.Any() ? p.ProductSpecs.First().SalePrice : decimal.MaxValue);
						break;
					default:
						query = query.OrderBy(p => p.ProductName);
						break;
				}
			}
			if (!string.IsNullOrEmpty(filterProductDTO.SearchString))
			{
				query = query.Where(p => p.ProductName.Contains(filterProductDTO.SearchString));
			}
			return await query.Skip((filterProductDTO.Page - 1) * filterProductDTO.PageSize).Take(filterProductDTO.PageSize).ToListAsync();
		}
		public async Task<long> GetTotalProductByCondition(FilterProductDTO filterProductDTO)
		{
			var query = _context.Products
				.Include(p => p.ProductImages)
				.Include(p => p.ProductSpecs).AsQueryable();
			if (filterProductDTO.CategoryId != 0)
			{
				query = query.Where(p => p.ProductCategories.Any(pc => pc.CategoryId == filterProductDTO.CategoryId));
			}
			if (!string.IsNullOrEmpty(filterProductDTO.SearchString))
			{
				query = query.Where(p => p.ProductName.Contains(filterProductDTO.SearchString));
			}
			return await query.CountAsync();
		}

		public async Task<List<Product>> GetProductByConditionForAdmin(FilterProductDTO filterProductDTO)
		{
			var query = _context.Products.AsQueryable();
			if (!string.IsNullOrEmpty(filterProductDTO.SearchString))
			{
				query = query.Where(p => p.ProductName.Contains(filterProductDTO.SearchString)
				|| (p.Description != null && p.Description.Contains(filterProductDTO.SearchString)));
			}
			if (!string.IsNullOrEmpty(filterProductDTO.Status))
			{
				query = query.Where(p => filterProductDTO.Status == "active" ? p.ProductStatus == true : p.ProductStatus == false);
			}
			return await query.Skip((filterProductDTO.Page - 1) * filterProductDTO.PageSize).Take(filterProductDTO.PageSize).ToListAsync();

		}

		public async Task<long> GetTotalProductByConditionForAdmin(FilterProductDTO filterProductDTO)
		{
			var query = _context.Products.AsQueryable();
			if (!string.IsNullOrEmpty(filterProductDTO.SearchString))
			{
				query = query.Where(p => p.ProductName.Contains(filterProductDTO.SearchString)
				|| (p.Description != null && p.Description.Contains(filterProductDTO.SearchString)));
			}
			if (!string.IsNullOrEmpty(filterProductDTO.Status))
			{
				query = query.Where(p => filterProductDTO.Status == "active" ? p.ProductStatus == true : p.ProductStatus == false);
			}
			return await query.CountAsync();
		}

		public async Task<Product> GetProductDetailById(int productId)
		{
			return await _context.Products.Include(p => p.ProductSpecs)
				 .Include(p => p.ProductImages)
				 .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
				 .FirstOrDefaultAsync(p => p.ProductId == productId) ?? new();
		}

		public async Task<bool> IsUpdateProductForAdmin(Product product)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				Product? existingProduct = await _context.Products.
					Include(p => p.ProductSpecs)
					.Include(p => p.ProductImages)
					.FirstOrDefaultAsync(p => p.ProductId == product.ProductId);
				if (existingProduct == null) throw new Exception();
				existingProduct.ProductName = product.ProductName;
				existingProduct.Description = product.Description;
				existingProduct.ProductStatus = product.ProductStatus;
				foreach (ProductSpec spec in product.ProductSpecs)
				{
					ProductSpec? existingSpec = await _context.ProductSpecs.FirstOrDefaultAsync(ps => ps.ProductSpecId == spec.ProductSpecId);
					if (existingSpec == null) throw new Exception();
					existingSpec.BasePrice = spec.BasePrice;
					existingSpec.SalePrice = spec.SalePrice;
					existingSpec.Quantity = spec.Quantity;
				}
				if(existingProduct.ProductImages != null && product.ProductImages != null)
				{
					for(int i = 0; i < existingProduct.ProductImages.Count; i++)
					{
						existingProduct.ProductImages.ElementAt(i).ImageUrl = product.ProductImages.ElementAt(i).ImageUrl;
					}
				}
				await _context.SaveChangesAsync();
				await transaction.CommitAsync();
			}
			catch (Exception)
			{
				await transaction.RollbackAsync();
				return false;
			}
			return true;
		}

		public async Task<bool> IsUpdateProductStatusById(int productId, bool status)
		{
			try
			{
				Product? existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
				if (existingProduct == null) throw new Exception();
				existingProduct.ProductStatus = status;
				await _context.SaveChangesAsync();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

        public void AddReview(Feedback feedback , int? orderItemId)
        {
            feedback.CreatedAt = DateTime.Now;
            feedback.Status = "Enable";
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();

			//Gán feedback vừa add cho orderitem luôn
            if (orderItemId.HasValue)
            {
                var orderItem = _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == orderItemId.Value);
                if (orderItem != null)
                {
                    orderItem.FeedbackId = feedback.FeedbackId;
                    _context.SaveChanges(); 
                }
            }

        }

        public async Task<List<Feedback>> GetReviews(int? productId)
        {
            return await _context.Feedbacks
    .Where(f => f.ProductId == productId && f.Status.Equals("Enable"))
    .Include(f => f.Medias) // nếu muốn kèm theo ảnh/video
    .Include(f => f.Customer) // nếu muốn kèm theo thông tin người gửi
    .OrderByDescending(f => f.CreatedAt)
    .ToListAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id)
        {
            return await _context.Feedbacks
                .Include(f => f.Customer)
                .Include(f => f.Medias)
                .FirstOrDefaultAsync(f => f.FeedbackId == id);
        }

		public async Task<long> GetTotalProductTypes() => await _context.Products.LongCountAsync();

		public async Task<List<ProductImage>> GetProductImagesByProductId(int productId)
		{
			return await _context.ProductImages.Where(p => p.ProductId == productId).ToListAsync();
		}

		public async Task<bool> IsCreateProductForAdmin(Product product)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				product.CreatedAt = DateTime.Now;
				product.ProductStatus = true;
				_context.Products.Add(product);	
				await _context.SaveChangesAsync();
				foreach (ProductSpec spec in product.ProductSpecs)
				{
					await _context.Database.ExecuteSqlRawAsync("Insert into ProductSpecs(SpecName, Quantity, BasePrice, SalePrice, ProductId, ProductStatus) values({0}, {1}, {2}, {3}, {4}, {5})", new object[]
					{
						spec.SpecName,
						spec.Quantity,
						spec.BasePrice,
						spec.SalePrice,
						product.ProductId,
						1
					});
				}


				foreach (ProductImage image in product.ProductImages)
				{
					await _context.Database.ExecuteSqlRawAsync("Insert into ProductImages(ProductId, ImageURL) Values({0}, {1})",
						new object[] { product.ProductId, image.ImageUrl });
				}
				foreach (ProductCategory category in product.ProductCategories)
				{
					await _context.Database.ExecuteSqlRawAsync("Insert into ProductCategories(ProductId, CategoryId) values({0}, {1})", new object[] { product.ProductId, category.CategoryId });
				}
				await _context.SaveChangesAsync();
				await transaction.CommitAsync();
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return false;
			}
			return true;
		}
	}
}
