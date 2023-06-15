using FluentValidation;
using TheStore.SharedModels.Models.Wishlist;

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