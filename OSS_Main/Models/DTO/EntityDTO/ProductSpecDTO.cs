namespace OSS_Main.Models.DTO.EntityDTO
{
	public class ProductSpecDTO
	{
		public int ProductSpecId { get; set; }

		public string SpecName { get; set; } = null!;

		public int Quantity { get; set; }

		public decimal BasePrice { get; set; }

		public decimal? SalePrice { get; set; }

		public int ProductId { get; set; }

		public bool ProductStatus { get; set; }
	}
}
