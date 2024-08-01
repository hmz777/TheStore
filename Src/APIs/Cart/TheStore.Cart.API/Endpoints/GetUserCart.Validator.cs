using FluentValidation;
using TheStore.Requests.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
    public class GetUserCartValidator : AbstractValidator<GetUserCartRequest>;
}