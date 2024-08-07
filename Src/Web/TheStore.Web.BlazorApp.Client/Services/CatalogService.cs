using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;
using TheStore.Web.BlazorApp.Client.Configuration;
using TheStore.Web.Requests.Products;

namespace TheStore.Web.BlazorApp.Client.Services
{
    public class CatalogService
    {
        private readonly IOptions<JsonSerializerOptions> jsonOptions;
        protected HttpClient? httpClient;

        public CatalogService(
            IOptions<JsonSerializerOptions> jsonOptions,
            IHttpClientFactory httpClientFactory)
        {
            this.jsonOptions = jsonOptions;

            if (OperatingSystem.IsBrowser())
            {
                httpClient = httpClientFactory.CreateClient(Constants.HttpClientConstants.ClientBackendHttpClient);
            }
            else
            {
                httpClient = httpClientFactory.CreateClient(Constants.HttpClientConstants.ServerCatalogHttpClient);
            }
        }

        public async Task<Result<ProductsPaginatedResult>> ListProductsPaginated(int take, int page, CancellationToken cancellationToken = default)
        {
            var request = new ListRequest() { Page = page, Take = take };

            var result = await httpClient!.
                GetFromJsonAsync<Result<ProductsPaginatedResult>>(request.Route, cancellationToken);

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

            var result = await httpClient!.GetFromJsonAsync<Result<ProductDetailsDtoRead>>(request.Route, cancellationToken);

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

            var result = await httpClient!.GetFromJsonAsync<Result<ProductReviewsPaginatedResult>>(request.Route, cancellationToken);

            if (result == null)
            {
                // TODO: Report errors to system usign some kind of reporting service
                throw new Exception("Catalog API returned null response");
            }

            return result;
        }
    }
}