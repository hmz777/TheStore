using AutoMapper;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.SharedModels.Models.Category;
using TheStore.SharedModels.Models.Products;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.Infrastructure.MappingProfiles
{
	public class CatalogMappingProfiles : Profile
	{
		public CatalogMappingProfiles()
		{
			// Categories
			CreateMap<Category, CategoryDto>()
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id.Id));

			CreateMap<SharedModels.Models.Category.CreateRequest, Category>();
			CreateMap<SharedModels.Models.Category.UpdateRequest, Category>();

			// Single Products
			CreateMap<SingleProduct, ProductDto>()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id.Id));

			CreateMap<SharedModels.Models.Products.CreateRequest, SingleProduct>()
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => new CategoryId(src.CategoryId)));

			CreateMap<SharedModels.Models.Products.UpdateRequest, SingleProduct>()
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => new CategoryId(src.CategoryId)));

			// Value Objects
			CreateMap<Money, MoneyDto>().ReverseMap();
			CreateMap<Currency, CurrencyDto>().ReverseMap();
			CreateMap<InventoryRecord, InventoryRecordDto>().ReverseMap();
			CreateMap<Image, ImageDto>().ReverseMap();
			CreateMap<ProductColor, ProductColorDto>().ReverseMap();
		}
	}
}