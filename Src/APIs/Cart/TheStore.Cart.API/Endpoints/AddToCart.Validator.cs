using FluentValidation;
using TheStore.Requests.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
	public class AddToCartValidator : AbstractValidator<AddToCartRequest>
	{
		public AddToCartValidator()
		{
			RuleFor(x => x.Sku)
				.NotEmpty();
		}
	}
}