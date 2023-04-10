using AutoFixture;
using AutoFixture.Xunit2;
using TheStore.Domain.Tests.AutoData.Customizations;

namespace TheStore.Domain.Tests.AutoData
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
