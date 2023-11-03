using Microsoft.EntityFrameworkCore;
using TheStore.ApiCommon.Interfaces;

namespace TheStore.Catalog.Infrastructure.Data
{
	public class DataSeeder : IDataSeeder<CatalogDbContext>
	{
		public async Task SeedDataAsync(CatalogDbContext context)
		{
			await InsertDataAsync(context);
			await context.SaveChangesAsync();
		}

		private async Task InsertDataAsync(CatalogDbContext context)
		{
			if (await context.Categories.AnyAsync() == false)
			{
				await context.Categories.AddRangeAsync(DummyDataHelper.GenerateDummyCategories());
			}

			if (await context.Products.AnyAsync() == false)
			{
				await context.Products.AddRangeAsync(DummyDataHelper.GenerateDummyProducts());
			}

			if (await context.Branches.AnyAsync() == false)
			{
				await context.Branches.AddRangeAsync(DummyDataHelper.GenerateDummyBranches());
			}
		}
	}
}
