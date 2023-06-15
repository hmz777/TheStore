using AutoFixture;
using AutoMapper;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Domain.UnitTests.AutoData.Customizations;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos
{
	public class InventoryRecordDtoCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new InventoryRecordCustomization());
			fixture.Register(() =>
			{
				var mapper = fixture.Create<IMapper>();
				var inventoryRecord = fixture.Create<InventoryRecord>();

				return mapper.Map<InventoryRecordDto>(inventoryRecord);
			});
		}
	}
}