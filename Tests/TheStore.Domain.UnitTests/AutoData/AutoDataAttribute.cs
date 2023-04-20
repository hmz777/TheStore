using AutoFixture;
using AutoFixture.Xunit2;
using TheStore.Domain.UnitTests.AutoData.Customizations;

namespace TheStore.Domain.UnitTests.AutoData
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
