using AutoFixture;
using TheStore.Endpoints.Tests.AutoData.Services;

namespace TheStore.Endpoints.Tests.AutoData.Dtos
{
	public class DtoCustomizations : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new ImageDtoCustomization());
			fixture.Customize(new InventoryRecordDtoCustomization());
			fixture.Customize(new MoneyDtoCustomization());
			fixture.Customize(new ProductColorDtoCustomization());
		}
	}
}
