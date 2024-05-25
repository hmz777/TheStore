using AutoMapper;
using TheStore.SharedModels.Models.Products;
using TheStore.Web.BlazorApp.Client.Pages.Catalog.Components;

namespace TheStore.Web.BlazorApp.Client.Configuration
{
	public class AutoMapperMappingProfile : Profile
	{
		public AutoMapperMappingProfile()
		{
			CreateMap<ProductVariantCatalogDtoRead, VariantSelectorModel>();
			CreateMap<ProductVariantDetailsDtoRead, VariantSelectorModel>();
		}
	}
}