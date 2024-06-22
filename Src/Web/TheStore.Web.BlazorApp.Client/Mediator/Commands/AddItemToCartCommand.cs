using Ardalis.GuardClauses;
using MediatR;

namespace TheStore.Web.BlazorApp.Client.Mediator.Commands
{
	public class AddItemToCartCommand : IRequest
	{
		public string Sku { get; }

		public AddItemToCartCommand(string sku)
		{
			Guard.Against.NullOrWhiteSpace(sku, nameof(sku));

			Sku = sku;
		}
	}
}