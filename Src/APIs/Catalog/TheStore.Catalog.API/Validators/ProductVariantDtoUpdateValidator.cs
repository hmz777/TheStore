using FluentValidation;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Validators
{
    public class ProductVariantDtoUpdateValidator : AbstractValidator<ProductVariantDtoUpdate>
	{
        public ProductVariantDtoUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Sku)
                .NotEmpty();

			RuleFor(x => x.Description)
				.NotEmpty()
				.SetValidator(ModelValidators.MultilanguageStringDtoValidator);

			RuleFor(x => x.ShortDescription)
				.NotEmpty()
				.SetValidator(ModelValidators.MultilanguageStringDtoValidator);

			RuleFor(x => x.Price)
				.NotEmpty()
				.SetValidator(ModelValidators.MoneyDtoValidator);

			RuleFor(x => x.Inventory)
				.NotEmpty()
				.SetValidator(ModelValidators.InventoryRecordDtoValidator);

			RuleFor(x => x.Color)
				.NotEmpty()
				.SetValidator(ModelValidators.ProductColorDtoUpdateValidator);
		}
    }
}
