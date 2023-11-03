using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class ProductVariantDtoValidator : AbstractValidator<ProductVariantDto>
	{
        public ProductVariantDtoValidator()
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
