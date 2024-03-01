using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;

namespace TheStore.TestHelpers.AutoData.Customizations
{
    public class AutoMapperCustomization : ICustomization
    {
        private readonly Profile mappingProfile;

        public AutoMapperCustomization(Profile mappingProfile)
        {
            this.mappingProfile = mappingProfile;
        }

        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new TypeRelay(typeof(IMapper), typeof(Mapper)));
            fixture.Register(() =>
            {
                var mapperConfig = new MapperConfiguration(x => x.AddProfile(mappingProfile));
                var mapper = new Mapper(mapperConfig);

                return mapper;
            });
        }
    }
}