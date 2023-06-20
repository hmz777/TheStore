using AutoFixture;
using TheStore.Catalog.Domain.UnitTests.AutoData.Customizations;

namespace TheStore.Catalog.Endpoints.UnitTests.AutoData.Endpoints
{
	public class EndpointsCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new DomainCustomization());
			fixture.Customize(new CategoryCustomization());
			fixture.Customize(new ProductCustomization());
			fixture.Customize(new BranchCustomization());
		}
	}
}