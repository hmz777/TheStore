using AutoFixture;
using TheStore.TestHelpers.AutoData.Specimens;

namespace TheStore.TestHelpers.AutoData.Customizations
{
	public class ProductColorDtoCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customizations.Add(new HexColorSpecimen());
		}
	}
}