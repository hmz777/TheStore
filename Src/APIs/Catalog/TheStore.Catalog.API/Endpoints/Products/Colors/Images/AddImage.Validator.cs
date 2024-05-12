using FluentValidation;
using TheStore.Catalog.API.Validators;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products.Colors.Images
{
	public class AddImageValidator : AbstractValidator<AddImageToVariantRequest>
	{
		public AddImageValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.Sku)
				.NotEmpty();

			RuleFor(x => x.Image)
				.NotEmpty()
				.SetValidator(ModelValidators.UploadImageDtoValidator);
		}
	}
}