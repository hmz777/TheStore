﻿using AutoMapper;
using AutoMapper.EquivalencyExpression;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;
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
			CreateMap<Category, CategoryDto>()
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id.Id));

			CreateMap<SharedModels.Models.Categories.CreateRequest, Category>();
			CreateMap<SharedModels.Models.Categories.UpdateRequest, Category>();

			// Single Products
			CreateMap<SingleProduct, SingleProductDto>()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id.Id))
				.ForMember(dest => dest.ProductColors, opt => opt.MapFrom("productColors"));

			CreateMap<SharedModels.Models.Products.CreateRequest, SingleProduct>()
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => new CategoryId(src.CategoryId)))
				.ForMember(dest => dest.ProductColors, opt => opt.Ignore());

			CreateMap<SharedModels.Models.Products.UpdateRequest, SingleProduct>()
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => new CategoryId(src.CategoryId)))
				.ForMember(dest => dest.ProductColors, opt => opt.Ignore());

			// Value Objects
			CreateMap<Money, MoneyDto>().ReverseMap();
			CreateMap<Currency, CurrencyDto>().ReverseMap();
			CreateMap<InventoryRecord, InventoryRecordDto>().ReverseMap();
			CreateMap<Image, ImageDto>().ReverseMap();
			CreateMap<UpdateImageDto, Image>()
				.EqualityComparison((src, dest) => src.ImageId == dest.Id)
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ImageId));

			CreateMap<ProductColor, ProductColorDto>()
				.ForMember(dest => dest.Images, opt => opt.MapFrom("images"))
				.ReverseMap()
				.ForMember("images", opt => opt.MapFrom(src => src.Images));

			CreateMap<UpdateProductColorDto, ProductColor>()
				.ForMember(dest => dest.Images, opt => opt.Ignore())
				.EqualityComparison((src, dest) => src.ProductColorId == dest.Id)
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductColorId));


			// Branches
			CreateMap<Branch, BranchDto>();
			CreateMap<SharedModels.Models.Branches.CreateRequest, Branch>();
			CreateMap<SharedModels.Models.Branches.UpdateRequest, Branch>();

			// Value Objects
			CreateMap<Address, AddressDto>().ReverseMap();
			CreateMap<Coordinate, CoordinateDto>().ReverseMap();
		}
	}
}