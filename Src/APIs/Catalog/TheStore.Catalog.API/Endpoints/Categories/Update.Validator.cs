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
				.NotEmpty();

			RuleFor(x => x.Order)
				.NotEmpty()
				.GreaterThanOrEqualTo(0);
		}
	}
}