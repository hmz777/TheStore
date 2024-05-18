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
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.MappingProfiles;
using TheStore.Requests.Models.Products;
using TheStore.TestHelpers.AutoData.Customizations;

namespace TheStore.Catalog.Endpoints.UnitTests.Products
{
	public class AssembledProductsSpec
	{
		[Fact]
		public async Task Can_List_Assembled_Products()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization(new CatalogMappingProfiles()));
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
			fixture.Customize(new AutoMapperCustomization(new CatalogMappingProfiles()));
			fixture.Customize(new EndpointsCustomization());

			var request = fixture.Create<GetAssembledByIdRequest>();
			var assembledProduct = fixture.Create<AssembledProduct>();
			assembledProduct.Id = request.ProductId;

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
			fixture.Customize(new AutoMapperCustomization(new CatalogMappingProfiles()));
			fixture.Customize(new EndpointsCustomization());

			var request = fixture.Create<DeleteAssembledRequest>();
			var assembledProduct = fixture.Create<AssembledProduct>();
			assembledProduct.Id = request.ProductId;

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, AssembledProduct>>();
			mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<ProductId>(), default))
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
			assembledProduct.Id = request.ProductId;

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
			assembledProduct.Id = fixture.Create<int>();

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, AssembledProduct>>();
			mockRepository.Setup(x => x.AddAsync(It.IsAny<AssembledProduct>(), default))
				.ReturnsAsync(assembledProduct);

			var sut = new Create(new CreateValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Result.Should().BeOfType(typeof(CreatedAtRouteResult));
		}

		[Fact]
		public async Task Can_Add_Part_To_Assembled_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<AddPartRequest>();
			var assembledProduct = fixture.Create<AssembledProduct>();
			assembledProduct.Id = request.ProductId;

			var singleProduct = fixture.Create<Product>();
			singleProduct.Id = new ProductId(request.PartId);

			var assembledProductRepository = new Mock<IApiRepository<CatalogDbContext, AssembledProduct>>();
			assembledProductRepository.Setup(x => x.GetByIdAsync(assembledProduct.Id, default))
				.ReturnsAsync(assembledProduct);

			var singleProductRepository = new Mock<IApiRepository<CatalogDbContext, Product>>();
			singleProductRepository.Setup(x => x.GetByIdAsync(singleProduct.Id, default))
				.ReturnsAsync(singleProduct);

			var sut = new AddPart(
				new AddPartValidator(),
				assembledProductRepository.Object,
				singleProductRepository.Object,
				fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(CreatedAtRouteResult));
		}

		[Fact]
		public async Task Can_Remove_Part_From_Assembled_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<RemovePartRequest>();
			var assembledProduct = fixture.Create<AssembledProduct>();
			assembledProduct.Id = request.ProductId;

			assembledProduct.AddPart(new ProductId(request.PartId), request.Sku);

			var assembledProductRepository = new Mock<IApiRepository<CatalogDbContext, AssembledProduct>>();
			assembledProductRepository.Setup(x => x.GetByIdAsync(assembledProduct.Id, default))
				.ReturnsAsync(assembledProduct);

			var sut = new RemovePart(
				new RemovePartValidator(),
				assembledProductRepository.Object);

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}
	}
}