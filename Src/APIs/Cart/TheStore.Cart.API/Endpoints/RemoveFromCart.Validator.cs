using FluentValidation;
using TheStore.Requests.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
    public class RemoveFromCartValidator : AbstractValidator<RemoveFromCartRequest>
    {
        public RemoveFromCartValidator()
        {
            RuleFor(x => x.Sku)
                .NotEmpty();
        }
    }
}