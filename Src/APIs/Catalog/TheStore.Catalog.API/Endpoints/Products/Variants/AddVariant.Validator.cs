using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products.Variants
{
	public class AddVariantValidator : AbstractValidator<AddVariantRequest>
	{
		public AddVariantValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.ProductVariant)
				.NotEmpty()
				.SetValidator(ModelValidators.ProductVariantDtoValidator);
		}
	}
}
