using Ardalis.GuardClauses;
using MediatR;

namespace TheStore.Web.Blazor.Mediator.Commands
{
	public class AddItemToCartCommand : IRequest
	{
		public int ItemId { get; }

		public AddItemToCartCommand(int itemId)
		{
			Guard.Against.NegativeOrZero(itemId, nameof(itemId));

			ItemId = itemId;
		}
	}
}
