using AutoFixture;
using TheStore.Domain.Tests.AutoData.Specimens;

namespace TheStore.Domain.Tests.AutoData.Customizations
{
	public class ImageCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customizations.Add(new FileUriSpecimen());
		}
	}
}