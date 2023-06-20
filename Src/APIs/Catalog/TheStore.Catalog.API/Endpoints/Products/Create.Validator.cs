using FluentValidation;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
	public class CreateValidator : AbstractValidator<CreateRequest>
	{
		public CreateValidator()
		{
			RuleFor(x => x.CategoryId)
				.NotEmpty();

			RuleFor(x => x.Name)
				.NotEmpty();

			RuleFor(x => x.Description)
				.NotEmpty();

			RuleFor(x => x.ShortDescription)
				.NotEmpty();

			RuleFor(x => x.Sku)
				.NotEmpty();

			RuleFor(x => x.Price)
				.NotEmpty();

			RuleFor(x => x.Inventory)
				.NotEmpty();
		}
	}
}
