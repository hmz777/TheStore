using FluentValidation;
using TheStore.SharedModels.Models.Categories;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class UpdateValidator : AbstractValidator<UpdateRequest>
	{
		public UpdateValidator()
		{
			RuleFor(x => x.CategoryId)
				.NotEmpty();

			RuleFor(x => x.Name)
				.NotEmpty()
				.MinimumLength(2);

			RuleFor(x => x.Order)
				.NotEmpty()
				.GreaterThanOrEqualTo(1);
		}
	}
}
