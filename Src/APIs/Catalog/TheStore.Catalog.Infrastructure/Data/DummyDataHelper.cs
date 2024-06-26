﻿using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Infrastructure.Data
{
	public static class DummyDataHelper
	{
		private const string stringData =
			"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

		public static string GenerateRandomString()
		{
			var rand = new Random();
			return stringData[..rand.Next(15, stringData.Length)];
		}

		public static string GenerateRandomSmallString()
		{
			var rand = new Random();
			return stringData[..rand.Next(15, 100)];
		}

		public static string GenerateRandomHexColor()
		{
			var random = new Random();
			return string.Format("#{0:X6}", random.Next(0x1000000));
		}

		public static List<Category> GenerateDummyCategories()
		{
			var categories = new List<Category>();

			for (int i = 0; i < 20; i++)
			{
				var category = new Category(i + 1, new MultilanguageString($"Category {i + 1}", CultureCode.English), true);

				categories.Add(category);
			}

			return categories;
		}

		public static List<Branch> GenerateDummyBranches()
		{
			var branches = new List<Branch>();

			for (int i = 0; i < 20; i++)
			{
				var branch = new Branch(
					$"Branch {i + 1}",
					new MultilanguageString(GenerateRandomString(), CultureCode.English),
					new Address("Syria", "Tartus", "Barranieh", $"ZIP{i}", new Coordinate(0, 0)),
					true);

				branches.Add(branch);
			}

			return branches;
		}

		public static List<Product> GenerateDummyProducts(int? number = null)
		{
			var rand = new Random();
			var randomNumber = rand.Next(5, 999);
			var randomProductNumber = rand.Next(1, number ?? 100);
			var randomVariantNumber = rand.Next(1, 5);
			var randomImageNumber = rand.Next(1, 10);

			var products = new List<Product>();

			for (int i = 0; i < randomProductNumber; i++)
			{
				var variants = new List<ProductVariant>();

				for (int j = 0; j < randomVariantNumber; j++)
				{
					var color = GenerateRandomHexColor().Replace("#", "");

					var images = new List<Image>();

					for (int m = 0; m < randomImageNumber; m++)
					{
						var imageColor = color.Replace("#", "");

						images.Add(
							new Image($"https://placehold.co/600x400/{imageColor}/{(imageColor == "FFFFFF" ? "000000" : "FFFFFF")}/png?text=Image+{m}",
							new MultilanguageString(GenerateRandomSmallString(), CultureCode.English),
							false));
					}

					var variant = new ProductVariant()
					{
						Name = GenerateRandomSmallString(),
						Sku = $"SKU {j}",
						Description = new MultilanguageString(GenerateRandomString(), CultureCode.English),
						ShortDescription = new MultilanguageString(GenerateRandomString(), CultureCode.English),
						Price = new Money((j + 1) * 500, Currency.Usd),
						Inventory = new InventoryRecord(i, 5, 100, 0, false),
						Sizes = [new("XXS", SizeStandard.EUStandard), new("XS", SizeStandard.EUStandard)],
						Color = new ProductColor(GenerateRandomString(), color, false, images),
						Options = new ProductVariantOptions { Published = true, CanBeFavorited = true, CanBePurchased = true },
						Dimentions = new Dimensions(randomNumber, randomNumber, randomNumber, UnitOfMeasure.Cm),
						Sepcifications = [
							new(new MultilanguageString("Name1", CultureCode.English), new MultilanguageString("Value1", CultureCode.English)),
							new(new MultilanguageString("Name2", CultureCode.English), new MultilanguageString("Value2", CultureCode.English)),
							new(new MultilanguageString("Name3", CultureCode.English), new MultilanguageString("Value3", CultureCode.English))
						]
					};

					variants.Add(variant);
				}

				var product = new Product(
					new CategoryId(1),
					GenerateRandomSmallString(),
					$"Identifier-{i}",
					new MultilanguageString(GenerateRandomSmallString(), CultureCode.English),
					new MultilanguageString(GenerateRandomString(), CultureCode.English),
					true,
					variants,
					[
						new(new ProductId(1), GenerateRandomSmallString(), DateTimeOffset.UtcNow, GenerateRandomString(), 1, "Jack 1", true),
						new(new ProductId(1), GenerateRandomSmallString(), DateTimeOffset.UtcNow, GenerateRandomString(), 2, "Jack 2", true),
						new(new ProductId(1), GenerateRandomSmallString(), DateTimeOffset.UtcNow, GenerateRandomString(), 3, "Jack 3", true),
						new(new ProductId(1), GenerateRandomSmallString(), DateTimeOffset.UtcNow, GenerateRandomString(), 4, "Jack 4", true),
						new(new ProductId(1), GenerateRandomSmallString(), DateTimeOffset.UtcNow, GenerateRandomString(), 5, "Jack 5", true)
					]);

				products.Add(product);
			}

			return products;
		}
	}
}