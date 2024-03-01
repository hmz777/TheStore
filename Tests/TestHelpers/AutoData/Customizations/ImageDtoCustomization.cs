using AutoFixture;
using TheStore.TestHelpers.AutoData.Specimens;

namespace TheStore.TestHelpers.AutoData.Customizations
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