using Ardalis.Specification;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheStore.ApiCommon.Data.Repository;
using TheStore.Cart.API.Endpoints;
using TheStore.Cart.Core.Aggregates;
using TheStore.Cart.Core.Entities;
using TheStore.Cart.Infrastructure.Data;
using TheStore.Cart.Infrastructure.MappingProfiles;
using TheStore.Cart.Infrastructure.Services;
using TheStore.Requests.Models.Wishlist;
using TheStore.TestHelpers.AutoData.Customizations;

namespace TheStore.Cart.Endpoints.UnitTests
{
	public class WishlistSpec
	{
		[Fact]
		public async Task Can_List_Wishlists()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization(new CartMappingProfiles()));
			var request = new ListRequest(1, 10);
			var mockRepository = new Mock<IReadApiRepository<CartDbContext, Wishlist>>();
			mockRepository.Setup(x => x.ListAsync(It.IsAny<Specification<Wishlist>>(), default))
				.ReturnsAsync(fixture.CreateMany<Wishlist>(request.Take).ToList());

			var mapper = fixture.Create<IMapper>();

			var sut = new ListWishlists(new ListWishlistsValidator(), mockRepository.Object, mapper);
			var result = (await sut.HandleAsync(request)).Value;

			result.Should().HaveCount(request.Take);
		}

		[Fact]
		public async Task Can_Get_Wishlist_By_Id()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization(new CartMappingProfiles()));
			var request = new GetWishlistByIdRequest();
			request.WishlistId = Guid.NewGuid();

			var wishlist = fixture.Create<Wishlist>();
			wishlist.Id = request.WishlistId;

			var mockRepository = new Mock<IReadApiRepository<CartDbContext, Wishlist>>();
			mockRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Specification<Wishlist>>(), default))
				.ReturnsAsync(wishlist);

			var mapper = fixture.Create<IMapper>();

			var sut = new GetWishlistById(new GetWishlistByIdValidator(), mockRepository.Object, mapper);
			var result = (await sut.HandleAsync(request)).Value;

			result.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Add_Item_To_Wishlist()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization(new CartMappingProfiles()));
			var request = new AddToWishlistRequest(Guid.NewGuid(), "Sku 0");
			var wishlist = fixture.Create<Wishlist>();
			wishlist.Id = request.WishlistId;

			var mockRepository = new Mock<IApiRepository<CartDbContext, Wishlist>>();
			mockRepository.Setup(x => x.GetByIdAsync(request.WishlistId, default))
				.ReturnsAsync(wishlist);

			var mockEntityCheckService = new Mock<ICatalogEntityCheckService>();
			mockEntityCheckService.Setup(x => x.CheckProductExistsAsync(request.Sku, default))
				.ReturnsAsync(true);

			var mapper = fixture.Create<IMapper>();

			var sut = new AddToWishlist(new AddToWishlistValidator(),
				mockEntityCheckService.Object,
				mockRepository.Object,
				mapper);

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(CreatedAtRouteResult));
		}

		[Fact]
		public async Task Can_Remove_Item_From_Wishlist()
		{
			var fixture = new Fixture();
			var request = new RemoveFromWishlistRequest(Guid.NewGuid(), "Sku 0");
			var wishlist = fixture.Create<Wishlist>();
			wishlist.AddItem(new WishlistItem(request.Sku));
			wishlist.Id = request.WishlistId;

			var mockRepository = new Mock<IApiRepository<CartDbContext, Wishlist>>();
			mockRepository.Setup(x => x.GetByIdAsync(request.WishlistId, default))
				.ReturnsAsync(wishlist);

			var sut = new RemoveFromWishlist(new RemoveFromWishlistValidator(), mockRepository.Object);
			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}
	}
}