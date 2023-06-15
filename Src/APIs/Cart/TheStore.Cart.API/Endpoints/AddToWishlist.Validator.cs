using FluentValidation;
using TheStore.SharedModels.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
	public class AddToWishlistValidator : AbstractValidator<AddToWishlistRequest>
	{
		public AddToWishlistValidator()
		{
			RuleFor(x => x.WishlistId)
				.NotEmpty();

			RuleFor(x => x.ItemId)
				.NotEmpty();
		}
	}
}