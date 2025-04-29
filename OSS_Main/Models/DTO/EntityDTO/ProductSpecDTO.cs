using OSS_Main.Utils;
using System.ComponentModel.DataAnnotations;

namespace OSS_Main.Models.DTO.EntityDTO
{
	public class ProductSpecDTO
	{
		public int ProductSpecId { get; set; }

		[Required(ErrorMessage = "Please enter spec name")]
		public string SpecName { get; set; } = null!;

		[Required(ErrorMessage = "Please enter quantity")]
		[Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
		public int Quantity { get; set; }

		[Required(ErrorMessage = "Please enter base price")]
		[Range(1000, double.MaxValue, ErrorMessage = "Base price must be at least 1000")]
		public decimal BasePrice { get; set; }

		[Required(ErrorMessage = "Please enter sale price")]
		[Range(1000, double.MaxValue, ErrorMessage = "Sale price must be at least 1000")]
		[SalePriceLessThanBasePrice]
		public decimal? SalePrice { get; set; }

		public int ProductId { get; set; }

		public bool ProductStatus { get; set; }
	}
}
