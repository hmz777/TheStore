using AutoFixture;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Core.ValueObjects;

namespace TheStore.Endpoints.Tests.AutoData.Endpoints
{
	public class BranchCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Register(() =>
			{
				return new Branch(
					fixture.Create<string>(),
					fixture.Create<string>(),
					fixture.Create<Address>(),
					fixture.Create<Image>());
			});
		}
	}
}