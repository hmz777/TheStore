using AutoFixture;

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