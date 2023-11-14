using AutoFixture;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos
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
