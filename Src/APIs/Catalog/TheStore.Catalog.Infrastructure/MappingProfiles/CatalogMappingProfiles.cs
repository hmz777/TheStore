using AutoMapper;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
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
			CreateMap<Category, CategoryDto>();
			CreateMap<SharedModels.Models.Category.CreateRequest, Category>();
			CreateMap<SharedModels.Models.Category.UpdateRequest, Category>();

			// Single Products
			CreateMap<SingleProduct, ProductDto>();
			CreateMap<SharedModels.Models.Products.CreateRequest, SingleProduct>();
			CreateMap<SharedModels.Models.Products.UpdateRequest, SingleProduct>();

			// Value Objects
			CreateMap<Money, MoneyDto>();
			CreateMap<InventoryRecord, InventoryRecordDto>();
			CreateMap<ProductColor, ProductColorDto>();
		}
	}
}