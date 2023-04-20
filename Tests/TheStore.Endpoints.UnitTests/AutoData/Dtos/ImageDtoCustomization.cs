using AutoFixture;
using AutoMapper;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Domain.UnitTests.AutoData.Customizations;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Endpoints.UnitTests.AutoData.Dtos
{
	public class ImageDtoCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new ImageCustomization());
			fixture.Register(() =>
			{
				var mapper = fixture.Create<IMapper>();
				var image = fixture.Create<Image>();

				return mapper.Map<ImageDto>(image);
			});
		}
	}
}
