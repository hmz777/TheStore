using AutoFixture;
using AutoFixture.Xunit2;

namespace TheStore.Catalog.Domain.UnitTests.AutoData
{
	public class DomainAutoDataAttribute : AutoDataAttribute
	{
		public DomainAutoDataAttribute() : base(() =>
		{
			var fixture = new Fixture();

			fixture.Customize(new InventoryRecordCustomization());
			fixture.Customize(new ImageCustomization());
			fixture.Customize(new ProductColorCustomization());

			return fixture;
		})
		{ }
	}
}
