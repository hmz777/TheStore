using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.SingleProducts
{
	public class UpdateValidator : AbstractValidator<UpdateRequest>
	{
		public UpdateValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

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

			RuleFor(x => x.ProductColors)
				.NotEmpty()
				.DependentRules(() => RuleForEach(x => x.ProductColors).SetValidator(x => new ProductColorValidator()));
		}
	}
}