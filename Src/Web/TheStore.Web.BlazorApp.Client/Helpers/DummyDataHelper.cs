namespace TheStore.Web.BlazorApp.Client.Helpers
{
	public static class DummyDataHelper
	{
		private static readonly string stringData = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

		public static int GenerateRandomNumber(int maxNumber)
		{
			var rand = new Random();
			return rand.Next(1, maxNumber);
		}

		public static string GenerateRandomString()
		{
			var rand = new Random();
			return stringData.Substring(0, rand.Next(15, stringData.Length));
		}

		public static string GenerateRandomSmallString()
		{
			var rand = new Random();
			return stringData.Substring(0, rand.Next(15, 100));
		}

		public static string GenerateRandomHexColor()
		{
			var random = new Random();
			return string.Format("#{0:X6}", random.Next(0x1000000));
		}

		//public static ProductDetailsDtoRead GenerateDummyProduct()
		//{
		//	var rand = new Random();
		//	var randomNumeber = rand.Next(5, 5000);
		//	var randomColorNumber = rand.Next(1, 10);
		//	var randomImageNumber = rand.Next(1, 10);

		//	var product = new ProductDetailsDtoRead
		//	{
		//		Description = new MultilanguageStringDto() { LocalizedStrings = [new LocalizedStringDto(new CultureCodeDto("en-US"), GenerateRandomString())] },
		//		Name = GenerateRandomSmallString(),
		//		Inventory = new InventoryRecordDto()
		//		{
		//			AvailableStock = randomNumeber
		//		},
		//		ProductColors = [],
		//		Price = new MoneyDto() { Amount = randomNumeber, Currency = new CurrencyDto() { CurrencyCode = "USD" } },
		//		ProductId = randomNumeber,
		//		ShortDescription = GenerateRandomString(),
		//		Sku = $"SKU{randomNumeber}"
		//	};

		//	for (int i = 0; i < randomColorNumber; i++)
		//	{
		//		var productColor = new ProductColorDtoRead()
		//		{
		//			ColorCode = GenerateRandomHexColor()
		//		};

		//		var images = new List<ImageDto>();

		//		for (int j = 0; j < randomImageNumber; j++)
		//		{
		//			var color = productColor.ColorCode.Replace("#", "");

		//			images.Add(new ImageDto()
		//			{
		//				Alt = new MultilanguageStringDto()
		//				{
		//					LocalizedStrings = [
		//						new LocalizedStringDto(new CultureCodeDto("en-US"), GenerateRandomString()),
		//						new LocalizedStringDto(new CultureCodeDto("ar-SY"), GenerateRandomString()),
		//					]
		//				},
		//				StringFileUri = $"https://placehold.co/600x400/{color}/{(color == "FFFFFF" ? "000000" : "FFFFFF")}/png?text=Image+{j}"
		//			});
		//		}

		//		productColor.Images = images;

		//		product.ProductColors.Add(productColor);
		//	}

		//	return product;
		//}
	}
}
