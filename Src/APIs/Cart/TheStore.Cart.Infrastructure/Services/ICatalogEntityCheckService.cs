namespace TheStore.Cart.Infrastructure.Services
{
	public interface ICatalogEntityCheckService
	{
		Task<bool> CheckBranchExistsAsync(string sku, CancellationToken cancellationToken = default);
		Task<bool> CheckCategoryExistsAsync(string sku, CancellationToken cancellationToken = default);
		Task<bool> CheckProductExistsAsync(string sku, CancellationToken cancellationToken = default);
	}
}