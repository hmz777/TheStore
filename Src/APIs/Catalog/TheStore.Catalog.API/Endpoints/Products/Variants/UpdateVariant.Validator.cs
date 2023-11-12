using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products.Variants
{
	public class UpdateVariantValidator : AbstractValidator<UpdateVariantRequest>
	{
		public UpdateVariantValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.Sku)
				.NotEmpty();

			RuleFor(x => x.Variant)
				.NotEmpty()
				.SetValidator(ModelValidators.ProductVariantDtoUpdateValidator);
		}
	}
}
