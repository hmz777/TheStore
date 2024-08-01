using MediatR;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Cart;

namespace TheStore.Web.BlazorApp.Client.Mediator.Queries
{
    public class GetCartQuery : IRequest<Result<CartDto>>;
}