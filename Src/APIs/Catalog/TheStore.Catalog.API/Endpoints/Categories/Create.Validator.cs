using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.Requests.Models.Categories;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class CreateValidator : AbstractValidator<CreateRequest>
	{
		public CreateValidator()
		{
			RuleFor(x => x.Category)
				.NotEmpty()
				.SetValidator(ModelValidators.CategoryDtoUpdateValidator);
		}
	}
}
