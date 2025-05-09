using OSS_Main.Models.DTO.EntityDTO;
using System.ComponentModel.DataAnnotations;

namespace OSS_Main.Utils
{
	public class NoDuplicateImageAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			try
			{
				var spec = (ProductDTO)validationContext.ObjectInstance;
				var urlHashList = new List<string>();
				foreach (var item in spec.ProductImages)
				{
					if (item.ImageFile == null || item.ImageFile.Length == 0) continue;
					var bytes = UtilHelper.GetBytesFromIFormFile(item.ImageFile);
					var hash = UtilHelper.ComputeSha256Hash(bytes);
					if (urlHashList.Contains(hash))
					{
						return new ValidationResult("Duplicate image");
					}
					urlHashList.Add(hash);
				}
				return ValidationResult.Success;
			}
			catch (Exception ex)
			{
				return new ValidationResult(ex.Message);
			}
		}
	}
}
