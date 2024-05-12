using AutoFixture;
using TheStore.SharedModels.Models.Products;

namespace TheStore.TestHelpers.AutoData.Customizations
{
	public class ProductReviewDtoCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Register(() =>
			{
				var review = new ProductReviewDto
				{
					Title = "Title",
					Content = "Content",
					Rating = new Random().Next(1, 5),
					Date = DateTimeOffset.Now,
					User = "User"
				};

				return review;
			});
		}
	}
}
