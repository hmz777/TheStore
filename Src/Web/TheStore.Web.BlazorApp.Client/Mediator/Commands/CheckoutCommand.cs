using MediatR;
using TheStore.SharedModels.Models;

namespace TheStore.Web.BlazorApp.Client.Mediator.Commands
{
    public class CheckoutCommand : IRequest<Result>
    {
    }
}
