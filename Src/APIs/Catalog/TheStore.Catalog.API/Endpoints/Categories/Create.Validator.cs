using FluentValidation;
using TheStore.SharedModels.Models.Category;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class CreateValidator : AbstractValidator<CreateRequest>
	{
		public CreateValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.MinimumLength(2);

			RuleFor(x => x.Order)
				.NotEmpty()
				.GreaterThanOrEqualTo(1);
		}
	}
}
