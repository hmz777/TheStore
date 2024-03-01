using AutoFixture;
using AutoMapper;
using TheStore.SharedKernel.ValueObjects;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.TestHelpers.AutoData.Customizations
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
