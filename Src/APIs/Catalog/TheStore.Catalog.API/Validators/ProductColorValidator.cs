using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class ProductColorValidator : AbstractValidator<ProductColorDto>
	{
		public ProductColorValidator()
		{
			RuleFor(x => x.ColorCode)
				.NotEmpty();

			RuleFor(x => x.Images)
				.NotEmpty()
				.DependentRules(() => RuleForEach(x => x.Images).SetValidator(x => new ImageValidator()));
		}
	}
}