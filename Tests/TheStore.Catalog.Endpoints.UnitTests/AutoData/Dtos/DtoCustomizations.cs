using AutoFixture;
using TheStore.Catalog.Infrastructure.MappingProfiles;
using TheStore.TestHelpers.AutoData.Services;

namespace TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos
{
	public class DtoCustomizations : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new AutoMapperCustomization(new CatalogMappingProfiles()));
			fixture.Customize(new ImageDtoCustomization());
			fixture.Customize(new InventoryRecordDtoCustomization());
			fixture.Customize(new MoneyDtoCustomization());
			fixture.Customize(new ProductColorDtoCustomization());
			fixture.Customize(new MultilanguageStringDtoCustomization());
			fixture.Customize(new ProductReviewDtoCustomization());
		}
	}
}
