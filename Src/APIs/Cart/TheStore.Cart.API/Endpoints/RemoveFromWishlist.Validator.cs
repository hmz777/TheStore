using FluentValidation;
using TheStore.SharedModels.Models.Wishlist;

namespace TheStore.Cart.API.Endpoints
{
	public class RemoveFromWishlistValidator : AbstractValidator<RemoveFromWishlistRequest>
	{
		public RemoveFromWishlistValidator()
		{
			RuleFor(x => x.WishlistId)
				.NotEmpty();

			RuleFor(x => x.ItemId)
				.NotEmpty();
		}
	}
}