using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;
using TheStore.Catalog.Infrastructure.MappingProfiles;

namespace TheStore.Endpoints.Tests.AutoData.ServiceCustomizations
{
    public class AutoMapperCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new TypeRelay(typeof(IMapper), typeof(Mapper)));
            fixture.Register(() =>
            {
                var mappingProfile = new CatalogMappingProfiles();
                var mapperConfig = new MapperConfiguration(x => x.AddProfile(mappingProfile));
                var mapper = new Mapper(mapperConfig);

                return mapper;
            });
        }
    }
}