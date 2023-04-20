using AutoFixture;
using TheStore.Domain.UnitTests.AutoData.Customizations;

namespace TheStore.Endpoints.UnitTests.AutoData.Endpoints
{
	public class EndpointsCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new DomainCustomization());
			fixture.Customize(new CategoryCustomization());
			fixture.Customize(new SingleProductCustomization());
			fixture.Customize(new BranchCustomization());
		}
	}
}