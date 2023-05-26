using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.SingleProducts.Colors.Images
{
	public class UpdateImageValidator : AbstractValidator<UpdateImageOfColorRequest>
	{
		public UpdateImageValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.ColorCode)
				.NotEmpty();

			RuleFor(x => x.ImagePath)
				.NotEmpty();

			RuleFor(x => x.Image)
				.NotEmpty()
				.SetValidator(x => new UpdateImageDtoValidator());
		}
	}
}
