using AutoFixture;
using TheStore.Domain.UnitTests.AutoData.Specimens;

namespace TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos
{
	public class ProductColorDtoCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customizations.Add(new HexColorSpecimen());
		}
	}
}