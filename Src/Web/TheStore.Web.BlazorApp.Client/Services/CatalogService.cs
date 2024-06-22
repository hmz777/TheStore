using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using TheStore.SharedModels.Models.Products;
using TheStore.Web.BlazorApp.Client.Configuration;
using TheStore.Web.Requests.Products;

namespace TheStore.Web.BlazorApp.Client.Services
{
	public class CatalogService(IOptions<ClientAppConfig> options, HttpClient httpClient)
	{
		private readonly string endpoint = options.Value.Endpoints.First(e => e.Name == "Catalog Api").Url;
		private readonly HttpClient httpClient = httpClient;

		public async Task<ProductsPaginatedResult> ListProductsPaginated(int take, int page, CancellationToken cancellationToken = default)
		{
			var request = new ListRequest() { Page = page, Take = take };

			var productsResult = await httpClient.GetFromJsonAsync<ProductsPaginatedResult>(endpoint + request.Route, cancellationToken);

			return productsResult ?? new ProductsPaginatedResult()
			{
				Products = [],
				PageNumber = page
			};
		}

		public async Task<ProductDetailsDtoRead> GetProductDetails(string identifier, CancellationToken cancellationToken = default)
		{
			var request = new GetByIdentifierRequest { Identifier = identifier };

			var product = await httpClient.GetFromJsonAsync<ProductDetailsDtoRead>(endpoint + request.Route, cancellationToken);

			// TODO: Report errors to system usign some kind of reporting service
			return product ?? throw new Exception("Couldn't fetch product details");
		}

		public async Task<ProductReviewsPaginatedResult> ListProductReviewsPaginated(string identifier, int take, int page, CancellationToken cancellationToken = default)
		{
			var request = new ListReviewsRequest { Identifier = identifier, Page = page, Take = take };

			var productReviewsResult = await httpClient.GetFromJsonAsync<ProductReviewsPaginatedResult>(endpoint + request.Route, cancellationToken);

			return productReviewsResult ?? new ProductReviewsPaginatedResult()
			{
				Reviews = [],
				PageNumber = page
			};
		}
	}
}