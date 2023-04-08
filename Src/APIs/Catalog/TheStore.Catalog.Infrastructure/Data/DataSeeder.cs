using Microsoft.EntityFrameworkCore;
using TheStore.ApiCommon.Interfaces;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.Infrastructure.Data
{
	public class DataSeeder : IDataSeeder<CatalogDbContext>
	{
		public async Task SeedDataAsync(CatalogDbContext context)
		{
			if (await context.Categories.AnyAsync() == false)
			{
				await context.Categories.AddRangeAsync(GenerateCategories());
			}

			if (await context.SingleProducts.AnyAsync() == false)
			{
				await context.SingleProducts.AddRangeAsync(GenerateSingleProducts());
			}

			if (await context.Branches.AnyAsync() == false)
			{
				await context.Branches.AddRangeAsync(GenerateBranches());
			}

			await context.SaveChangesAsync();
		}

		private List<Category> GenerateCategories()
		{
			var categories = new List<Category>();

			for (int i = 0; i < 20; i++)
			{
				categories.Add(new Category(i + 1, $"Category {i + 1}", true));
			}

			return categories;
		}

		private List<SingleProduct> GenerateSingleProducts()
		{
			var singleProducts = new List<SingleProduct>();

			for (int i = 0; i < 20; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					singleProducts.Add(
						new SingleProduct(
							new CategoryId(i),
							$"Product {j}",
							"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
							"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
							$"SKU{i}{j}",
							new Money(50 * i * j, Currency.Usd),
							new InventoryRecord(50, 5, 100, 0, false)));
				}
			}

			return singleProducts;
		}

		private List<Branch> GenerateBranches()
		{
			var branches = new List<Branch>();

			for (int i = 0; i < 20; i++)
			{
				branches.Add(
					new Branch(
					$"Branch {i + 1}",
					"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
					new Address("Syria", "Tartus", "Barranieh", $"ZIP{i}", new Coordinate(0, 0)),
					new Image("https://placehold.co/600x400.png", "Placeholder image")));
			}

			return branches;
		}
	}
}
