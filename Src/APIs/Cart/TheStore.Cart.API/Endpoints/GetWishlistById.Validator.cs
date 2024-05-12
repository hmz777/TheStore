using FluentValidation;
using TheStore.Requests.Models.Wishlist;

namespace TheStore.Cart.API.Endpoints
{
	public class GetWishlistByIdValidator : AbstractValidator<GetWishlistByIdRequest>
	{
		public GetWishlistByIdValidator()
		{
			RuleFor(x => x.WishlistId)
				.NotEmpty();
		}
	}
}