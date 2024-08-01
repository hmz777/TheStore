using MediatR;
using TheStore.SharedModels.Models;

namespace TheStore.Web.BlazorApp.Client.Mediator.Commands
{
    // TODO: Result should be a SaveCartResult containing all the different consts and the final price including the currency
    public class SaveCartCommand : IRequest<Result<decimal>>;
}