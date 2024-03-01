using AutoFixture;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.TestHelpers.AutoData.Customizations
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
