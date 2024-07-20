namespace TheStore.Cart.Infrastructure.Services
{
	public class CatalogEntityCheckService : ICatalogEntityCheckService
	{
		private readonly CatalogEntityChecks.CatalogEntityChecksClient catalogEntityChecksClient;

		public CatalogEntityCheckService(
			CatalogEntityChecks.CatalogEntityChecksClient catalogEntityChecksClient)
		{
			this.catalogEntityChecksClient = catalogEntityChecksClient;
		}

		public virtual async Task<bool> CheckBranchExistsAsync(string sku, CancellationToken cancellationToken = default)
		{
			var result = await catalogEntityChecksClient
				.BranchExistsAsync(new CheckRequest() { Sku = sku }, cancellationToken: cancellationToken);

			return result.Result;
		}

		public virtual async Task<bool> CheckCategoryExistsAsync(string sku, CancellationToken cancellationToken = default)
		{
			var result = await catalogEntityChecksClient
				.CategoryExistsAsync(new CheckRequest() { Sku = sku }, cancellationToken: cancellationToken);

			return result.Result;
		}

		public virtual async Task<bool> CheckProductExistsAsync(string sku, CancellationToken cancellationToken = default)
		{
			var result = await catalogEntityChecksClient
				.ProductExistsAsync(new CheckRequest() { Sku = sku }, cancellationToken: cancellationToken);

			return result.Result;
		}
	}
}