using AutoFixture;
using TheStore.Domain.UnitTests.AutoData.Specimens;

namespace TheStore.Domain.UnitTests.AutoData.Customizations
{
	public class ImageCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customizations.Add(new FileUriSpecimen());
		}
	}
}