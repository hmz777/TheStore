using FluentValidation;
using TheStore.Requests.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
	public class GetCartByIdValidator : AbstractValidator<GetCartByIdRequest>
	{
		public GetCartByIdValidator()
		{
			RuleFor(x => x.CartId)
				.NotEmpty();
		}
	}
}