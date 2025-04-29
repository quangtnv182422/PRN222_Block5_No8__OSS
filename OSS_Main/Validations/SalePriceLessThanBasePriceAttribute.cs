using OSS_Main.Models.DTO.EntityDTO;
using System.ComponentModel.DataAnnotations;

namespace OSS_Main.Validations
{
	public class SalePriceLessThanBasePriceAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var spec = (ProductSpecDTO)validationContext.ObjectInstance;
			if (spec.SalePrice > spec.BasePrice || spec.SalePrice == spec.BasePrice)
			{
				return new ValidationResult(ErrorMessage ?? "Sale price must be less than base price.");
			}

			return ValidationResult.Success;
		}
	}
}
