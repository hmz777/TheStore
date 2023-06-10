using Ardalis.Specification;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheStore.ApiCommon.Data.Repository;
using TheStore.Catalog.API.Endpoints.AssembledProducts;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Endpoints;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Services;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.Endpoints.UnitTests.Products
{
	public class AssembledProductsSpec
	{
		[Fact]
		public async Task Can_List_Assembled_Products()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = new ListAssembledRequest(1, 10);

			var mockRepository = new Mock<IReadApiRepository<CatalogDbContext, AssembledProduct>>();
			mockRepository.Setup(x => x.ListAsync(It.IsAny<Specification<AssembledProduct>>(), default))
				.ReturnsAsync(fixture.CreateMany<AssembledProduct>(request.Take).ToList());

			var sut = new List(new ListValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = (await sut.HandleAsync(request)).Value;

			result.Should().HaveCount(request.Take);
		}

		[Fact]
		public async Task Can_Get_Assembled_Product_By_Id()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = fixture.Create<GetAssembledByIdRequest>();
			var assembledProduct = fixture.Create<AssembledProduct>();
			assembledProduct.Id = new AssembledProductId(request.ProductId);

			var mockRepository = new Mock<IReadApiRepository<CatalogDbContext, AssembledProduct>>();
			mockRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Specification<AssembledProduct>>(), default))
				.ReturnsAsync(assembledProduct);

			var sut = new GetById(new GetByIdValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = (await sut.HandleAsync(request)).Value;

			result.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Delete_Assembled_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = fixture.Create<DeleteAssembledRequest>();
			var assembledProduct = fixture.Create<AssembledProduct>();
			assembledProduct.Id = new AssembledProductId(request.ProductId);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, AssembledProduct>>();
			mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<AssembledProductId>(), default))
				.ReturnsAsync(assembledProduct);

			var sut = new Delete(new DeleteValidator(), mockRepository.Object);

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		[Fact]
		public async Task Can_Update_Assembled_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<UpdateAssembledRequest>();
			var assembledProduct = fixture.Create<AssembledProduct>();
			assembledProduct.Id = new AssembledProductId(request.ProductId);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, AssembledProduct>>();
			mockRepository.Setup(x => x.GetByIdAsync(assembledProduct.Id, default))
				.ReturnsAsync(assembledProduct);

			var sut = new Update(new UpdateValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		[Fact]
		public async Task Can_Create_Assembled_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<CreateAssembledRequest>();
			var assembledProduct = fixture.Create<AssembledProduct>();
			assembledProduct.Id = new AssembledProductId(fixture.Create<int>());

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, AssembledProduct>>();
			mockRepository.Setup(x => x.AddAsync(It.IsAny<AssembledProduct>(), default))
				.ReturnsAsync(assembledProduct);

			var sut = new Create(new CreateValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Result.Should().BeOfType(typeof(CreatedAtRouteResult));
		}
	}
}