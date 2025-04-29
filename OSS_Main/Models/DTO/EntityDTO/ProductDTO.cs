using OSS_Main.Utils;
using System.ComponentModel.DataAnnotations;

namespace OSS_Main.Models.DTO.EntityDTO
{
	public class ProductDTO
	{
		public int ProductId { get; set; }

		[Required(ErrorMessage = "Please enter product name")]
		public string ProductName { get; set; } = null!;
		[Required(ErrorMessage = "Please enter description")]
		public string? Description { get; set; }

		public DateTime CreatedAt { get; set; }

		public bool ProductStatus { get; set; }

		[NoDuplicateImage]
		public List<ProductImageDTO> ProductImages { get; set; } = new();

		public List<ProductSpecDTO> ProductSpecs { get; set; } = new();
		public List<ProductCategoryDTO> ProductCategories { get; set; } = new();
	}
}
