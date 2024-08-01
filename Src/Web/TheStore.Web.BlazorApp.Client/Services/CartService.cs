using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Cart;
using TheStore.Web.BlazorApp.Client.Configuration;
using TheStore.Web.BlazorApp.Client.Helpers;
using TheStore.Web.Requests.Cart;

namespace TheStore.Web.BlazorApp.Client.Services
{
    public class CartService(
        IOptions<ClientAppConfig> options,
        IOptions<JsonSerializerOptions> jsonOptions,
        HttpClient httpClient,
        EventBroker eventBroker)
    {
        private readonly string endpoint = options.Value.Endpoints
            .First(e => e.Name == Constants.Endpoints.CartEndpointConfigKey).Url;

        public async Task<Result> AddItemToCart(string sku, CancellationToken cancellationToken = default)
        {
            var request = new AddToCartRequest(sku);

            var response = await httpClient
                .PostAsJsonAsync(endpoint + request.Route, request, cancellationToken);

            var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            var result = JsonSerializer
                .Deserialize<Result<CartItemDto>>(jsonResponse, jsonOptions.Value);

            if (result == null)
            {
                // TODO: Report the error to some error reporting service
                return Result.Failure("Cart API responded with null");
            }

            eventBroker.OnItemAddedToCart?.Invoke(result);

            return Result.Success("Item has been added to cart");
        }

        public async Task<Result<CartDto>> GetCart(CancellationToken cancellationToken = default)
        {
            var request = new GetUserCartRequest();

            var response = await httpClient
                .GetFromJsonAsync<Result<CartDto>>(endpoint + request.Route, cancellationToken);

            if (response == null)
            {
                // TODO: Report the error to some error reporting service
                return Result.Failure<CartDto>(null!, "Unexpected response from Cart API");
            }

            return response;
        }
    }
}