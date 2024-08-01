using Ardalis.GuardClauses;
using MediatR;
using TheStore.SharedModels.Models;

namespace TheStore.Web.BlazorApp.Client.Mediator.Commands
{
    public class RemoveItemFromCartCommand : IRequest<Result>
    {
        public string Sku { get; }

        public RemoveItemFromCartCommand(string sku)
        {
            Guard.Against.NullOrEmpty(sku, nameof(sku));

            Sku = sku;
        }
    }
}