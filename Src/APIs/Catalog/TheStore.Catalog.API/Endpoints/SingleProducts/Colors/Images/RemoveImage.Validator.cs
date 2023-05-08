using FluentValidation;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.SingleProducts.Colors.Images
{
	public class RemoveImageValidator : AbstractValidator<RemoveImageFromColorRequest>
	{
		public RemoveImageValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.ProductColorId)
				.NotEmpty();

			RuleFor(x => x.ImageId)
				.NotEmpty();
		}
	}
}