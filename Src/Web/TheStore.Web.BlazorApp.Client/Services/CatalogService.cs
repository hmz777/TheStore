using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;
using TheStore.Web.BlazorApp.Client.Configuration;
using TheStore.Web.BlazorApp.Client.Helpers;
using TheStore.Web.Requests.Products;

namespace TheStore.Web.BlazorApp.Client.Services
{
    public class CatalogService(IOptions<ClientAppConfig> options, HttpClient httpClient)
    {
        private readonly string endpoint = options.Value.Endpoints
            .First(e => e.Name == Constants.Endpoints.CatalogEndpointConfigKey).Url;

        public async Task<Result<ProductsPaginatedResult>> ListProductsPaginated(int take, int page, CancellationToken cancellationToken = default)
        {
            var request = new ListRequest() { Page = page, Take = take };

            var result = await httpClient.
                GetFromJsonAsync<Result<ProductsPaginatedResult>>(endpoint + request.Route, cancellationToken);

            if (result == null)
            {
                // TODO: Report errors to system usign some kind of reporting service
                throw new Exception("Catalog API returned null response");
            }

            return result;
        }

        public async Task<Result<ProductDetailsDtoRead>> GetProductDetails(
            string identifier, CancellationToken cancellationToken = default)
        {
            var request = new GetByIdentifierRequest { Identifier = identifier };

            var result = await httpClient.GetFromJsonAsync<Result<ProductDetailsDtoRead>>(endpoint + request.Route, cancellationToken);

            if (result == null)
            {
                // TODO: Report errors to system usign some kind of reporting service
                throw new Exception("Catalog API returned null response");
            }

            return result;
        }

        public async Task<Result<ProductReviewsPaginatedResult>> ListProductReviewsPaginated(string identifier, int take, int page, CancellationToken cancellationToken = default)
        {
            var request = new ListReviewsRequest { Identifier = identifier, Page = page, Take = take };

            var result = await httpClient.GetFromJsonAsync<Result<ProductReviewsPaginatedResult>>(endpoint + request.Route, cancellationToken);

            if (result == null)
            {
                // TODO: Report errors to system usign some kind of reporting service
                throw new Exception("Catalog API returned null response");
            }

            return result;
        }
    }
}