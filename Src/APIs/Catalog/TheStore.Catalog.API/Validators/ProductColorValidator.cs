using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class UpdatedProductColorValidator : AbstractValidator<UpdateProductColorDto>
	{
		public UpdatedProductColorValidator()
		{
			RuleFor(x => x.ColorCode)
				.NotEmpty();

			RuleFor(x => x.Images)
				.NotEmpty()
				.DependentRules(() =>
				{
					RuleForEach(x => x.Images)
						.SetValidator(x => new ImageValidator());
				});
		}
	}
}