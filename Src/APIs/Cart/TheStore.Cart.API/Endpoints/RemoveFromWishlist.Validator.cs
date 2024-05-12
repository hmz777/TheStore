using FluentValidation;
using TheStore.Requests.Models.Wishlist;

namespace TheStore.Cart.API.Endpoints
{
	public class RemoveFromWishlistValidator : AbstractValidator<RemoveFromWishlistRequest>
	{
		public RemoveFromWishlistValidator()
		{
			RuleFor(x => x.WishlistId)
				.NotEmpty();

			RuleFor(x => x.ProductId)
				.NotEmpty();
		}
	}
}