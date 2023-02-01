using AutoFixture;
using TheStore.Tests.Domain.AutoData.Specimens;

namespace TheStore.Tests.Domain.AutoData.Customizations
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