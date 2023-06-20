using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products.Colors
{
	public class UpdateColorsValidator : AbstractValidator<UpdateColorRequest>
	{
		public UpdateColorsValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.Color)
				.NotEmpty()
				.SetValidator(x => new UpdatedProductColorDtoValidator());
		}
	}
}