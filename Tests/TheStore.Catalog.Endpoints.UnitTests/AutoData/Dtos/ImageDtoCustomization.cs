using AutoFixture;
using TheStore.APICommon.UnitTests.AutoData;

namespace TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos
{
	public class ImageDtoCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new MockFormFileCustomization());
			fixture.Customizations.Add(new FileUriSpecimen());
		}
	}
}