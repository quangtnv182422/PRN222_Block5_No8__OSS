using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;
using OSS_Main.Service.Interface;
using System.Collections.Generic;
using System.Drawing.Printing;

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
            return await _productRepo.AddProductWithImages(product,  SpecName,  SalePrice,  BasePrice,  Quantity,  productImages);
        }
    }

}
