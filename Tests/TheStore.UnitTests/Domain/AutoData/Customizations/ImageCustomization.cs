using AutoFixture;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Tests.Domain.AutoData.Specimens;

namespace TheStore.Tests.Domain.AutoData.Customizations
{
	public class ImageCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customizations.Add(new FileUriSpecimen());
		}
	}
}