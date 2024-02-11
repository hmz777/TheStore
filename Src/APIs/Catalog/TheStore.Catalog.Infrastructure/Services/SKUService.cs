using TheStore.ApiCommon.Data.Repository;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Data.Specifications.Products;

namespace TheStore.Catalog.Infrastructure.Services
{
	public class SkuService(IApiRepository<CatalogDbContext, Product> apiRepository)
	{
		private readonly IApiRepository<CatalogDbContext, Product> apiRepository = apiRepository;

		public async Task<string> CreateSkuAsync()
		{
			// Check if it's taken
			var products = await apiRepository.ListAsync(new GetAllProductsReadSpec());

			string sku = string.Empty;

			while (string.IsNullOrEmpty(sku) ||
				   products.Any(p => p.Variants.Where(v => v.Sku == sku).Any())) { sku = GenerateSku(); }

			return sku;
		}

		private string GenerateSku()
		{
			var rand = new Random();
			var chars = new char[4];
			var numbers = new int[4];

			for (var i = 0; i < 4; i++)
			{
				chars[i] = Convert.ToChar(rand.Next(65, 90));
				numbers[i] = rand.Next(0, 9);
			}

			var sku = $"{string.Join("", chars)}-{string.Join("", numbers)}";

			return sku;
		}
	}
}