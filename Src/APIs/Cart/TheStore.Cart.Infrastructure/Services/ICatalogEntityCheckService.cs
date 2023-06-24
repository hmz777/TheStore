namespace TheStore.Cart.Infrastructure.Services
{
	public interface ICatalogEntityCheckService
	{
		Task<bool> CheckBranchExistsAsync(int id, CancellationToken cancellationToken = default);
		Task<bool> CheckCategoryExistsAsync(int id, CancellationToken cancellationToken = default);
		Task<bool> CheckProductExistsAsync(int id, CancellationToken cancellationToken = default);
	}
}