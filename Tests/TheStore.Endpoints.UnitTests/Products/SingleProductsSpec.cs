using Ardalis.Specification;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheStore.ApiCommon.Data.Repository;
using TheStore.Catalog.API.Endpoints.SingleProducts;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Endpoints.UnitTests.AutoData.Dtos;
using TheStore.Endpoints.UnitTests.AutoData.Endpoints;
using TheStore.Endpoints.UnitTests.AutoData.Services;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Endpoints.UnitTests.Products
{
	public class SingleProductsSpec
	{
		[Fact]
		public async Task Can_List_Single_Products()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = new ListRequest(1, 10);

			var mockRepository = new Mock<IReadApiRepository<CatalogDbContext, SingleProduct>>();
			mockRepository.Setup(x => x.ListAsync(It.IsAny<Specification<SingleProduct>>(), default))
				.ReturnsAsync(fixture.CreateMany<SingleProduct>(request.Take).ToList());

			var sut = new List(new ListValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = (await sut.HandleAsync(request)).Value;

			result.Should().HaveCount(request.Take);
		}

		[Fact]
		public async Task Can_Get_Single_Product_By_Id()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = fixture.Create<GetByIdRequest>();
			var singleProduct = fixture.Create<SingleProduct>();
			singleProduct.Id = new ProductId(request.ProductId);

			var mockRepository = new Mock<IReadApiRepository<CatalogDbContext, SingleProduct>>();
			mockRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Specification<SingleProduct>>(), default))
				.ReturnsAsync(singleProduct);

			var sut = new GetById(new GetByIdValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = (await sut.HandleAsync(request)).Value;

			result.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Delete_Single_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = fixture.Create<DeleteRequest>();
			var singleProduct = fixture.Create<SingleProduct>();
			singleProduct.Id = new ProductId(request.ProductId);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, SingleProduct>>();
			mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<ProductId>(), default))
				.ReturnsAsync(singleProduct);

			var sut = new Delete(new DeleteValidator(), mockRepository.Object);

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		[Fact]
		public async Task Can_Update_Single_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<UpdateRequest>();
			var singleProduct = fixture.Create<SingleProduct>();
			singleProduct.Id = new ProductId(request.ProductId);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, SingleProduct>>();
			mockRepository.Setup(x => x.GetByIdAsync(singleProduct.Id, default))
				.ReturnsAsync(singleProduct);

			var sut = new Update(new UpdateValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		[Fact]
		public async Task Can_Create_Single_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<CreateRequest>();
			var singleProduct = fixture.Create<SingleProduct>();
			singleProduct.Id = new ProductId(fixture.Create<int>());

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, SingleProduct>>();
			mockRepository.Setup(x => x.AddAsync(It.IsAny<SingleProduct>(), default))
				.ReturnsAsync(singleProduct);

			var sut = new Create(new CreateValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Result.Should().BeOfType(typeof(CreatedAtRouteResult));
		}
	}
}
