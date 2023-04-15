using AutoFixture;

namespace TheStore.Endpoints.Tests.AutoData.DomainCustomizations
{
	public class DomainCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new CategoryCustomization());
		}
	}
}