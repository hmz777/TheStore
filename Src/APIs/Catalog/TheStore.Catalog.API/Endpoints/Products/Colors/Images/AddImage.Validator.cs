using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products.Colors.Images
{
	public class AddImageValidator : AbstractValidator<AddImageToColorRequest>
	{
		public AddImageValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.ColorCode)
				.NotEmpty();

			RuleFor(x => x.Image)
				.NotEmpty()
				.SetValidator(x => new AddImageDtoValidator());
		}
	}
}
