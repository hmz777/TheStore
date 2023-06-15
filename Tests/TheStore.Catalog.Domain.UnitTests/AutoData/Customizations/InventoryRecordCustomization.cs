using AutoFixture;
using TheStore.Catalog.Core.ValueObjects;

namespace TheStore.Catalog.Domain.UnitTests.AutoData.Customizations
{
	public class InventoryRecordCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Register(() =>
			{
				var rand = new Random();
				return new InventoryRecord(rand.Next(1, 500), 1, 501, 0, false);
			});
		}
	}
}
