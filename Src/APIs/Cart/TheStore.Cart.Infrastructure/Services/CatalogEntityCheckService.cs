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

		public virtual async Task<bool> CheckBranchExistsAsync(int id, CancellationToken cancellationToken = default)
		{
			var result = await catalogEntityChecksClient
				.BranchExistsAsync(new CheckRequest() { Id = id }, cancellationToken: cancellationToken);

			return result.Result;
		}

		public virtual async Task<bool> CheckCategoryExistsAsync(int id, CancellationToken cancellationToken = default)
		{
			var result = await catalogEntityChecksClient
				.CategoryExistsAsync(new CheckRequest() { Id = id }, cancellationToken: cancellationToken);

			return result.Result;
		}

		public virtual async Task<bool> CheckProductExistsAsync(int id, CancellationToken cancellationToken = default)
		{
			var result = await catalogEntityChecksClient
				.ProductExistsAsync(new CheckRequest() { Id = id }, cancellationToken: cancellationToken);

			return result.Result;
		}
	}
}