using AutoFixture;
using TheStore.Catalog.Core.Aggregates.Products;

namespace TheStore.TestHelpers.AutoData.Customizations
{
	public class ProductReviewCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Register(() =>
			{
				return new ProductReview(
					fixture.Create<string>(),
					fixture.Create<DateTimeOffset>(),
					fixture.Create<string>(),
					new Random().Next(1, 5),
					fixture.Create<string>(), true);
			});
		}
	}
}
