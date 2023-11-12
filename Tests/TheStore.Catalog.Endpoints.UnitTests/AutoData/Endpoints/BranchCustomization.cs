using AutoFixture;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Domain.UnitTests.AutoData.Customizations;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Endpoints.UnitTests.AutoData.Endpoints
{
	public class BranchCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new MultilanguageStringCustomization());
			fixture.Register(() =>
			{
				return new Branch(
					fixture.Create<string>(),
					fixture.Create<MultilanguageString>(),
					fixture.Create<Address>(),
					true);
			});
		}
	}
}