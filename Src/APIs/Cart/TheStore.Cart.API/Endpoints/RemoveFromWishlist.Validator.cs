using FluentValidation;
using TheStore.SharedModels.Models.Cart;

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