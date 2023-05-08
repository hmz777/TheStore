using FluentValidation;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.SingleProducts.Colors
{
    public class RemoveColorValidator : AbstractValidator<RemoveColorRequest>
    {
        public RemoveColorValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();

			RuleFor(x => x.ProductColorId)
				.NotEmpty();
		}
    }
}