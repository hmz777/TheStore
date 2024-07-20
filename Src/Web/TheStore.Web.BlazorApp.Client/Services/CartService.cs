using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using TheStore.SharedModels.Models;
using TheStore.Web.BlazorApp.Client.Configuration;
using TheStore.Web.BlazorApp.Client.Helpers;
using TheStore.Web.Requests.Cart;

namespace TheStore.Web.BlazorApp.Client.Services
{
	public class CartService(
		IOptions<ClientAppConfig> options,
		HttpClient httpClient,
		EventBroker eventBroker)
	{
		private readonly string endpoint = options.Value.Endpoints
			.First(e => e.Name == Constants.Endpoints.CartEndpointConfigKey).Url;

		public async Task AddItemToCart(string sku, CancellationToken cancellationToken = default)
		{
			var request = new AddToCartRequest(sku);

			var response = await httpClient.
				PostAsJsonAsync(endpoint + request.Route, request, cancellationToken);

			if (response.IsSuccessStatusCode)
			{
				eventBroker.OnItemAddedToCart?
					.Invoke(new Result(Status.Success, "Item has been added to cart"));
			}
			else
			{
				eventBroker.OnItemAddedToCart?
					.Invoke(new Result(Status.Failure, "Failed to add item to cart"));
			}
		}
	}
}