using AutoMapper;
using TheStore.Cart.Core.Aggregates;
using TheStore.Cart.Core.Entities;
using TheStore.SharedModels.Models.Cart;
using TheStore.SharedModels.Models.Wishlist;

namespace TheStore.Cart.Infrastructure.MappingProfiles
{
	public class CartMappingProfiles : Profile
	{
		public CartMappingProfiles()
		{
			CreateMap<CartItem, CartItemDto>();
			CreateMap<Core.Aggregates.Cart, CartDto>()
				.ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.ToList()));

			CreateMap<WishlistItem, WishlistItemDto>();
			CreateMap<Wishlist, WishlistDto>()
				.ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.ToList()));
		}
	}
}
