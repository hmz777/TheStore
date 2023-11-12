using AutoFixture;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Domain.UnitTests.AutoData.Customizations
{
	public class LocalizedStringCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Register(() =>
			{
				var localizedString = new LocalizedString(fixture.Create<string>(), CultureCode.English);

				return localizedString;
			});
		}
	}
}
