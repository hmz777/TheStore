using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.Requests.Models.Categories;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class UpdateValidator : AbstractValidator<UpdateRequest>
	{
		public UpdateValidator()
		{
			RuleFor(x => x.CategoryId)
				.NotEmpty();

			RuleFor(x => x.Category)
				.NotEmpty()
				.SetValidator(ModelValidators.CategoryDtoUpdateValidator);
		}
	}
}