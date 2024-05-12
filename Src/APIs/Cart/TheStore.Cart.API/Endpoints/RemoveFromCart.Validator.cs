using FluentValidation;
using TheStore.Requests.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
	public class RemoveFromCartValidator : AbstractValidator<RemoveFromCartRequest>
	{
		public RemoveFromCartValidator()
		{
			RuleFor(x => x.CartId)
				.NotEmpty();

			RuleFor(x => x.ProductId)
				.NotEmpty();
		}
	}
}