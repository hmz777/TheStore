using AutoFixture;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.TestHelpers.AutoData.Customizations
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