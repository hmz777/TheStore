using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.SingleProducts.Colors
{
	public class AddColorValidator : AbstractValidator<AddColorRequest>
	{
		public AddColorValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.Color)
				.NotEmpty()
				.SetValidator(x => new AddProductColorDtoValidator());
		}
	}
}