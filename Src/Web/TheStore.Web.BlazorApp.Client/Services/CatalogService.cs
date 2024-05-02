using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using TheStore.Web.BlazorApp.Client.Configuration;
using TheStore.Web.Models.Products;

namespace TheStore.Web.BlazorApp.Client.Services
{
	public class CatalogService(IOptions<ClientAppConfig> options, HttpClient httpClient)
	{
		private readonly string endpoint = options.Value.Endpoints.First(e => e.Name == "Catalog Api").Url;
		private readonly HttpClient httpClient = httpClient;

		public async Task<ProductsPaginatedResult> GetProductsPaginated(int take, int page, CancellationToken cancellationToken = default)
		{
			var request = new ListRequest() { Page = page, Take = take };

			//var productsResult = await httpClient.GetFromJsonAsync<ProductsPaginatedResult>(endpoint + request.Route, cancellationToken);
			var productsResult = await httpClient.GetFromJsonAsync<ProductsPaginatedResult>(endpoint + request.Route, cancellationToken);

			return productsResult ?? new ProductsPaginatedResult()
			{
				Products = [],
				PageNumber = page
			};
		}
	}
}