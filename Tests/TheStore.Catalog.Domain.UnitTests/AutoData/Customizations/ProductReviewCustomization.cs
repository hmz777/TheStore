using AutoFixture;
using TheStore.Catalog.Core.ValueObjects;

namespace TheStore.Catalog.Domain.UnitTests.AutoData.Customizations
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
					fixture.Create<string>());
			});
		}
	}
}
