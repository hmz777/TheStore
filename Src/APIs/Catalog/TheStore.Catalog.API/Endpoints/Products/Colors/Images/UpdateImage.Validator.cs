using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products.Colors.Images
{
	public class UpdateImageValidator : AbstractValidator<UpdateImageOfVariantRequest>
	{
		public UpdateImageValidator()
		{
			RuleFor(x => x.Identifier)
				.NotEmpty();

			RuleFor(x => x.Sku)
				.NotEmpty();

			RuleFor(x => x.ImagePath)
				.NotEmpty();

			RuleFor(x => x.Image)
				.NotEmpty()
				.SetValidator(ModelValidators.UploadImageDtoValidator);
		}
	}
}