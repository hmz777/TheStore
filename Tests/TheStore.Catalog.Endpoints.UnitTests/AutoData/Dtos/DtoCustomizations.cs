using AutoFixture;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Services;

namespace TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos
{
	public class DtoCustomizations : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new ImageDtoCustomization());
			fixture.Customize(new InventoryRecordDtoCustomization());
			fixture.Customize(new MoneyDtoCustomization());
			fixture.Customize(new ProductColorDtoCustomization());
		}
	}
}
