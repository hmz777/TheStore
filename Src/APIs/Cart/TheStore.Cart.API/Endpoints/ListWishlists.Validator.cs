using FluentValidation;
using TheStore.Requests.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
	public class ListWishlistsValidator : AbstractValidator<ListRequest>
	{
		public ListWishlistsValidator()
		{
			RuleFor(x => x.Page)
				.NotEmpty()
				.GreaterThanOrEqualTo(1);

			RuleFor(x => x.Take)
				.NotEmpty()
				.GreaterThanOrEqualTo(1);
		}
	}
}