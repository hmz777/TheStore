using Ardalis.Specification;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheStore.ApiCommon.Data.Repository;
using TheStore.Catalog.API.Endpoints.SingleProducts;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Endpoints;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Services;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Mediator.Handlers.ImageUpload;
using TheStore.SharedModels.Models.Products;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.Endpoints.UnitTests.Products
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

		[Fact]
		public async Task Can_Update_Single_Product_Colors()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			var mapper = fixture.Create<IMapper>();

			var request = fixture.Create<UpdateColorsRequest>();

			var newColors = fixture.CreateMany<UpdateProductColorDto>(5).ToList();
			newColors.ForEach(i => i.ProductColorId = default);
			newColors.SelectMany(c => c.Images).ToList().ForEach(i => i.ImageId = default);

			var presentColors = fixture.CreateMany<UpdateProductColorDto>(5).ToList();
			int id1 = 0, id2 = 0;
			presentColors.ForEach(i => i.ProductColorId = ++id1);
			presentColors.SelectMany(c => c.Images).ToList().ForEach(i => i.ImageId = ++id2);

			request.ProductColors = new List<UpdateProductColorDto>(newColors.Union(presentColors));

			var singleProduct = new SingleProduct(
				new CategoryId(1),
				fixture.Create<string>(),
				fixture.Create<string>(),
				fixture.Create<string>(),
				fixture.Create<string>(),
				fixture.Create<Money>(),
				fixture.Create<InventoryRecord>(),
				mapper.Map<List<ProductColor>>(presentColors));

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, SingleProduct>>();
			mockRepository.Setup(x => x.GetByIdAsync(new ProductId(request.ProductId), default))
				.ReturnsAsync(singleProduct);

			var mockMediator = new Mock<IMediator>();

			var sut = new UpdateColor(new UpdateColorsValidator(), mockRepository.Object, fixture.Create<IMapper>(), mockMediator.Object);

			var result = await sut.HandleAsync(request);

			mockMediator.Verify(x => x.Send(It.IsAny<UpdateImagesRequest>(), default), Times.Once);
			mockMediator.Verify(x => x.Send(It.IsAny<AddImagesRequest>(), default), Times.Once);
			request.ProductColors.SelectMany(x => x.Images).Any(i => string.IsNullOrEmpty(i.StringFileUri)).Should().BeFalse();
			singleProduct.ProductColors.Count.Should().Be(presentColors.Count + newColors.Count);
			result.Should().BeOfType(typeof(NoContentResult));
		}
	}
}