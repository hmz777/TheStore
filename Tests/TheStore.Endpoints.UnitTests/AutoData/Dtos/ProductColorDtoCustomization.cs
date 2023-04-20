using AutoFixture;
using AutoMapper;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.Domain.UnitTests.AutoData.Customizations;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Endpoints.UnitTests.AutoData.Dtos
{
	public class ProductColorDtoCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new ProductColorCustomization());
			fixture.Register(() =>
			{
				var mapper = fixture.Create<IMapper>();
				var productColor = fixture.Create<ProductColor>();

				return mapper.Map<ProductColorDto>(productColor);
			});
		}
	}
}
