using FluentValidation;
using TheStore.SharedModels.Models.Categories;

namespace TheStore.Catalog.API.Validators
{
	public class CategoryDtoUpdateValidator : AbstractValidator<CategoryDtoUpdate>
	{
		public CategoryDtoUpdateValidator()
		{
			RuleFor(x => x.Order)
				.NotEmpty()
				.GreaterThanOrEqualTo(1);

			RuleFor(x => x.Name)
				.NotEmpty()
				.SetValidator(ModelValidators.MultilanguageStringDtoValidator);
		}
	}
}
