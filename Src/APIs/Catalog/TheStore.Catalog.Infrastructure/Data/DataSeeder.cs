using Microsoft.EntityFrameworkCore;
using TheStore.ApiCommon.Constants;
using TheStore.ApiCommon.Interfaces;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;

namespace TheStore.Catalog.Infrastructure.Data
{
	public class DataSeeder : IDataSeeder<CatalogDbContext>
	{
		public async Task SeedDataAsync(CatalogDbContext context)
		{
			await InsertData(context, false);
			await context.SaveChangesAsync();
		}

		private async Task InsertData(CatalogDbContext context, bool insertKeys = false)
		{
			if (await context.Categories.AnyAsync() == false)
			{
				await context.Categories.AddRangeAsync(GenerateCategories(insertKeys));
			}

			if (await context.SingleProducts.AnyAsync() == false)
			{
				await context.SingleProducts.AddRangeAsync(GenerateSingleProducts(insertKeys));
			}

			if (await context.Branches.AnyAsync() == false)
			{
				await context.Branches.AddRangeAsync(GenerateBranches(insertKeys));
			}
		}

		private List<Category> GenerateCategories(bool insertKeys)
		{
			var categories = new List<Category>();

			for (int i = 0; i < 20; i++)
			{
				var category = new Category(i + 1, $"Category {i + 1}", true);

				if (insertKeys)
				{
					category.Id = new CategoryId(i);
				}

				categories.Add(category);
			}

			return categories;
		}

		private List<SingleProduct> GenerateSingleProducts(bool insertKeys)
		{
			var singleProducts = new List<SingleProduct>();

			for (int i = 0; i < 20; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					var singleProduct = new SingleProduct(
							new CategoryId(i + 1),
							$"Product {j}",
							"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
							"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
							$"SKU{i}{j}",
							new Money(50 * i * j, Currency.Usd),
							new InventoryRecord(50, 5, 100, 0, false),
							new List<ProductColor>
							{
								new ProductColor("#000000",new List<Image>()
								{
									new Image(@$"{ResourceFilePaths.ProductsImages}\file.png","image alt")
								})
							});

					if (insertKeys)
					{
						singleProduct.Id = new ProductId(i + 1);
					}

					singleProducts.Add(singleProduct);
				}
			}

			return singleProducts;
		}

		private List<Branch> GenerateBranches(bool insertKeys)
		{
			var branches = new List<Branch>();

			for (int i = 0; i < 20; i++)
			{
				var branch = new Branch(
					$"Branch {i + 1}",
					"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
					new Address("Syria", "Tartus", "Barranieh", $"ZIP{i}", new Coordinate(0, 0)),
					new Image(@$"{ResourceFilePaths.BranchesImages}\file.png", "Placeholder image"));

				if (insertKeys)
				{
					branch.Id = i + 1;
				}

				branches.Add(branch);
			}

			return branches;
		}
	}
}
