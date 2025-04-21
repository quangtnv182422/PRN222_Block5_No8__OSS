namespace OSS_Main.Models.DTO.FilterDTO
{
	public class FilterProductDTO
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
		public string SortingCategory { get; set; } = "";
		public string SearchString { get; set; } = "";
		public int CategoryId { get; set; }
	}
}
