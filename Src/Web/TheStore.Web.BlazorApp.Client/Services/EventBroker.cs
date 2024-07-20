using TheStore.SharedModels.Models;

namespace TheStore.Web.BlazorApp.Client.Services
{
	public class EventBroker
	{
		public Action<Result>? OnItemAddedToCart;
	}
}