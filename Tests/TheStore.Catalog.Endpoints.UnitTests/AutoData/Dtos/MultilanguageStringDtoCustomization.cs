using AutoFixture;
using AutoMapper;
using TheStore.Catalog.Domain.UnitTests.AutoData.Customizations;
using TheStore.SharedKernel.ValueObjects;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos
{
	public class MultilanguageStringDtoCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new MultilanguageStringCustomization());
			fixture.Register(() =>
			{
				var mapper = fixture.Create<IMapper>();
				var multilanguageString = fixture.Create<MultilanguageString>();

				return mapper.Map<MultilanguageStringDto>(multilanguageString);
			});
		}
	}
}
