using AutoMapper;
using TheStore.Catalog.API.Domain.Categories;
using TheStore.SharedModels.Models.Category;

namespace TheStore.Catalog.Infrastructure.MappingProfiles
{
    public class CatalogMappingProfiles : Profile
    {
        public CatalogMappingProfiles()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}