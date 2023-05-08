using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.SingleProducts.Colors.Images
{
	public class AddImageValidator : AbstractValidator<AddImageToColorRequest>
	{
		public AddImageValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.ProductColorId)
				.NotEmpty();

			RuleFor(x => x.Image)
				.NotEmpty()
				.SetValidator(x => new AddImageDtoValidator());
		}
	}
}
