namespace OSS_Main.Models.DTO.EntityDTO
{
	public class ProductDTO
	{
		public int ProductId { get; set; }

		public string ProductName { get; set; } = null!;

		public string? Description { get; set; }

		public DateTime CreatedAt { get; set; }

		public bool ProductStatus { get; set; }
		public List<ProductImageDTO>? ProductImages { get; set; }

		public List<ProductSpecDTO>? ProductSpecs { get; set; }

		public List<ProductCategoryDTO>? ProductCategories { get; set; }
	}
}
