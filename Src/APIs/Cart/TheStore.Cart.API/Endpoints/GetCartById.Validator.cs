using FluentValidation;
using TheStore.SharedModels.Models.Cart;

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