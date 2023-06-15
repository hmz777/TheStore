using AutoFixture;

namespace TheStore.Catalog.Domain.UnitTests.AutoData.Customizations
{
	public class ImageCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customizations.Add(new FileUriSpecimen());
		}
	}
}