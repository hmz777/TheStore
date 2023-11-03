using FluentValidation;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products.Colors.Images
{
	public class RemoveImageValidator : AbstractValidator<RemoveImageFromColorRequest>
	{
		public RemoveImageValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.Sku)
				.NotEmpty();

			RuleFor(x => x.ImagePath)
				.NotEmpty();
		}
	}
}