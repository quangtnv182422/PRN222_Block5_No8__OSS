namespace OSS_Main.Models.DTO.EntityDTO
{
	public class ProductCategoryDTO
	{
		public int ProductId { get; set; }

		public int CategoryId { get; set; }
		public CategoryDTO Category { get; set; }
	}
}
