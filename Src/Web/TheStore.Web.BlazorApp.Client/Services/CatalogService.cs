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

		public async Task<ProductsPaginatedResult> GetProductsPaginated(int take, int page, CancellationToken cancellationToken = default)
		{
			var request = new ListRequest() { Page = page, Take = take };

			var productsResult = await httpClient.GetFromJsonAsync<ProductsPaginatedResult>(endpoint + request.Route, cancellationToken);

			return productsResult ?? new ProductsPaginatedResult()
			{
				Products = [],
				PageNumber = page
			};
		}

		public async Task<ProductCatalogDtoRead> GetProductDetails(int id, CancellationToken cancellationToken = default)
		{
			var request = new GetByIdRequest() { ProductId = id };

			var product = await httpClient.GetFromJsonAsync<ProductCatalogDtoRead>(endpoint + request.Route, cancellationToken);

			// TODO: Report errors to system usign some kind of reporting service
			return product ?? throw new Exception("Couldn't fetch product details");
		}
	}
}