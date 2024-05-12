using AutoMapper;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.SharedKernel.ValueObjects;
using TheStore.SharedModels.Models.Branches;
using TheStore.SharedModels.Models.Categories;
using TheStore.SharedModels.Models.Products;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.Infrastructure.MappingProfiles
{
	public class CatalogMappingProfiles : Profile
	{
		public CatalogMappingProfiles()
		{
			// Categories
			CreateMap<Category, CategoryDtoRead>()
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id.Id));

			CreateMap<Category, CategoryDtoUpdate>().ReverseMap();

			// Products
			CreateMap<Product, ProductCatalogDtoRead>()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id.Id))
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId.Id));

			CreateMap<ProductDtoUpdate, Product>()
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => new CategoryId(src.CategoryId)))
				.ReverseMap()
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId.Id));

			CreateMap<ProductVariant, ProductVariantCatalogDtoRead>();
			CreateMap<ProductVariant, ProductVariantDetailsDtoRead>();
			CreateMap<ProductVariantDtoUpdate, ProductVariant>()
				.ForMember(dest => dest.Sku, opt => opt.Ignore());

			CreateMap<ProductVariantOptions, ProductVariantOptionsDto>().ReverseMap();

			// Assembled Products
			CreateMap<AssembledProduct, AssembledProductDtoRead>()
				.ForMember(dest => dest.Parts,
					opt => opt.MapFrom(src => src.Parts.ToDictionary(x => x.Key.Id, x => x.Value)));

			CreateMap<AssembledProductDtoUpdate, AssembledProduct>()
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => new CategoryId(src.CategoryId)));

			// Branches
			CreateMap<Branch, BranchDtoRead>();
			CreateMap<BranchDtoUpdate, Branch>().ReverseMap();

			// Value Objects

			CreateMap<LocalizedString, LocalizedStringDto>().ReverseMap();
			CreateMap<CultureCode, CultureCodeDto>().ReverseMap();

			CreateMap<MultilanguageString, MultilanguageStringDto>()
				.ForMember(dest => dest.LocalizedStrings, opt => opt.MapFrom("localizedStrings"))
				.ReverseMap()
				.ForMember("localizedStrings", opt => opt.MapFrom(src => src.LocalizedStrings))
				.ForMember(dest => dest.LocalizedStrings, opt => opt.Ignore());

			CreateMap<Money, MoneyDto>().ReverseMap();
			CreateMap<Currency, CurrencyDto>().ReverseMap();
			CreateMap<InventoryRecord, InventoryRecordDto>().ReverseMap();
			CreateMap<Image, ImageDto>().ReverseMap();

			CreateMap<ProductColor, ProductColorDtoRead>();

			CreateMap<ProductColorDtoUpdate, ProductColor>()
				.ForMember(dest => dest.Images, opt => opt.Ignore());

			CreateMap<Address, AddressDto>().ReverseMap();
			CreateMap<Coordinate, CoordinateDto>().ReverseMap();
			CreateMap<Dimensions, DimentionsDto>().ReverseMap();
			CreateMap<ProductReview, ProductReviewDto>().ReverseMap();
			CreateMap<UnitOfMeasure, UnitOfMeasureDto>().ReverseMap();
			CreateMap<ProductSpecification, ProductSpecificationDto>().ReverseMap();
		}
	}
}