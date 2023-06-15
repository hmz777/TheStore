using AutoFixture;
using TheStore.Catalog.Domain.UnitTests.AutoData.Specimens;

namespace TheStore.Catalog.Domain.UnitTests.AutoData.Customizations
{
	public class ProductColorCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customizations.Add(new HexColorSpecimen());
			fixture.Customize(new ImageCustomization());
		}
	}
}