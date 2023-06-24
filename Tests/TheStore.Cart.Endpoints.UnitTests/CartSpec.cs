using Ardalis.Specification;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheStore.ApiCommon.Data.Repository;
using TheStore.Cart.API.Endpoints;
using TheStore.Cart.Core.Entities;
using TheStore.Cart.Infrastructure.Data;
using TheStore.Cart.Infrastructure.MappingProfiles;
using TheStore.Cart.Infrastructure.Services;
using TheStore.SharedModels.Models.Cart;
using TheStore.TestHelpers.AutoData.Services;

namespace TheStore.Cart.Endpoints.UnitTests
{
	public class CartSpec
	{
		[Fact]
		public async Task Can_List_Carts()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization(new CartMappingProfiles()));
			var request = new ListRequest(1, 10);
			var mockRepository = new Mock<IReadApiRepository<CartDbContext, Core.Aggregates.Cart>>();
			mockRepository.Setup(x => x.ListAsync(It.IsAny<Specification<Core.Aggregates.Cart>>(), default))
				.ReturnsAsync(fixture.CreateMany<Core.Aggregates.Cart>(request.Take).ToList());

			var mapper = fixture.Create<IMapper>();

			var sut = new ListCarts(new ListCartsValidator(), mockRepository.Object, mapper);
			var result = (await sut.HandleAsync(request)).Value;

			result.Should().HaveCount(request.Take);
		}

		[Fact]
		public async void Can_Get_Cart_By_Id()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization(new CartMappingProfiles()));
			var request = new GetCartByIdRequest();
			request.CartId = Guid.NewGuid();

			var cart = fixture.Create<Core.Aggregates.Cart>();
			cart.Id = request.CartId;

			var mockRepository = new Mock<IReadApiRepository<CartDbContext, Core.Aggregates.Cart>>();
			mockRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Specification<Core.Aggregates.Cart>>(), default))
				.ReturnsAsync(cart);

			var mapper = fixture.Create<IMapper>();

			var sut = new GetCartById(new GetCartByIdValidator(), mockRepository.Object, mapper);
			var result = (await sut.HandleAsync(request)).Value;

			result.Should().NotBeNull();

		}

		[Fact]
		public async void Can_Add_Item_To_Cart()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization(new CartMappingProfiles()));
			var request = new AddToCartRequest(Guid.NewGuid(), 1);
			var cart = fixture.Create<Core.Aggregates.Cart>();
			cart.Id = request.CartId;

			var mockRepository = new Mock<IApiRepository<CartDbContext, Core.Aggregates.Cart>>();
			mockRepository.Setup(x => x.GetByIdAsync(request.CartId, default))
				.ReturnsAsync(cart);

			var mockEntityCheckService = new Mock<ICatalogEntityCheckService>();
			mockEntityCheckService.Setup(x => x.CheckProductExistsAsync(request.ProductId, default))
				.ReturnsAsync(true);

			var mapper = fixture.Create<IMapper>();

			var sut = new AddToCart(new AddToCartValidator(),
				mockEntityCheckService.Object,
				mockRepository.Object,
				mapper);

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(CreatedAtRouteResult));
		}

		[Fact]
		public async Task Can_Remove_Item_From_Cart()
		{
			var fixture = new Fixture();
			var request = new RemoveFromCartRequest(Guid.NewGuid(), 1);
			var cart = fixture.Create<Core.Aggregates.Cart>();
			cart.AddItem(new CartItem(request.ProductId, 1));
			cart.Id = request.CartId;

			var mockRepository = new Mock<IApiRepository<CartDbContext, Core.Aggregates.Cart>>();
			mockRepository.Setup(x => x.GetByIdAsync(request.CartId, default))
				.ReturnsAsync(cart);

			var sut = new RemoveFromCart(new RemoveFromCartValidator(), mockRepository.Object);
			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}
	}
}