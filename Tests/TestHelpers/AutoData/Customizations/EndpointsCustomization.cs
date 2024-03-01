using AutoFixture;

namespace TheStore.TestHelpers.AutoData.Customizations
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