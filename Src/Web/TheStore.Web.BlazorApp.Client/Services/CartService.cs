using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Cart;
using TheStore.Web.BlazorApp.Client.Configuration;
using TheStore.Web.Requests.Cart;

namespace TheStore.Web.BlazorApp.Client.Services
{
    public class CartService
    {
        private readonly IOptions<JsonSerializerOptions> jsonOptions;
        private readonly EventBroker eventBroker;
        protected HttpClient? httpClient;

        public CartService(
            IOptions<JsonSerializerOptions> jsonOptions,
            IHttpClientFactory httpClientFactory,
            EventBroker eventBroker)
        {
            this.jsonOptions = jsonOptions;
            this.eventBroker = eventBroker;

            if (OperatingSystem.IsBrowser())
            {
                httpClient = httpClientFactory.CreateClient(Constants.HttpClientConstants.ClientBackendHttpClient);
            }
            else
            {
                httpClient = httpClientFactory.CreateClient(Constants.HttpClientConstants.ServerAuthenticatedCartHttpClient);
            }
        }

        // TODO: Fix endpoint in Cart API
        public async Task<Result> AddItemToCart(string sku, CancellationToken cancellationToken = default)
        {
            var request = new AddToCartRequest(sku);

            var response = await httpClient!
                .PostAsJsonAsync(request.Route, request, cancellationToken);

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

            var response = await httpClient!
                .GetFromJsonAsync<Result<CartDto>>(request.Route, cancellationToken);

            if (response == null)
            {
                // TODO: Report the error to some error reporting service
                return Result.Failure<CartDto>(null!, "Unexpected response from Cart API");
            }

            return response;
        }
    }
}