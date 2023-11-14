using Ardalis.Specification;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheStore.ApiCommon.Data.Repository;
using TheStore.Catalog.API.Endpoints.Products;
using TheStore.Catalog.API.Endpoints.Products.Colors.Images;
using TheStore.Catalog.API.Endpoints.Products.Variants;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Domain.UnitTests.AutoData.Customizations;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Endpoints;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.MappingProfiles;
using TheStore.Catalog.Infrastructure.Mediator.Handlers.ImageUpload;
using TheStore.SharedKernel.ValueObjects;
using TheStore.SharedModels.Models.Products;
using TheStore.TestHelpers.AutoData.Services;

namespace TheStore.Catalog.Endpoints.UnitTests.Products
{
	public class ProductsSpec
	{
		#region Single Products

		[Fact]
		public async Task Can_List_Products()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization(new CatalogMappingProfiles()));
			fixture.Customize(new EndpointsCustomization());

			var request = new ListRequest(1, 10);

			var mockRepository = new Mock<IReadApiRepository<CatalogDbContext, Product>>();
			mockRepository.Setup(x => x.ListAsync(It.IsAny<Specification<Product>>(), default))
				.ReturnsAsync(fixture.CreateMany<Product>(request.Take).ToList());

			var sut = new List(new ListValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = (await sut.HandleAsync(request)).Value;

			result.Should().HaveCount(request.Take);
		}

		[Fact]
		public async Task Can_Get_Product_By_Id()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization(new CatalogMappingProfiles()));
			fixture.Customize(new EndpointsCustomization());

			var request = fixture.Create<GetByIdRequest>();
			var singleProduct = fixture.Create<Product>();
			singleProduct.Id = new ProductId(request.ProductId);

			var mockRepository = new Mock<IReadApiRepository<CatalogDbContext, Product>>();
			mockRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Specification<Product>>(), default))
				.ReturnsAsync(singleProduct);

			var sut = new GetById(new GetByIdValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = (await sut.HandleAsync(request)).Value;

			result.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Delete_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization(new CatalogMappingProfiles()));
			fixture.Customize(new EndpointsCustomization());

			var request = fixture.Create<DeleteRequest>();
			var singleProduct = fixture.Create<Product>();
			singleProduct.Id = new ProductId(request.ProductId);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Product>>();
			mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<ProductId>(), default))
				.ReturnsAsync(singleProduct);

			var sut = new Delete(new DeleteValidator(), mockRepository.Object);

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		[Fact]
		public async Task Can_Update_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<UpdateRequest>();
			var singleProduct = fixture.Create<Product>();
			singleProduct.Id = new ProductId(request.ProductId);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Product>>();
			mockRepository.Setup(x => x.GetByIdAsync(singleProduct.Id, default))
				.ReturnsAsync(singleProduct);

			var sut = new Update(new UpdateValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		[Fact]
		public async Task Can_Create_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<CreateRequest>();
			var singleProduct = fixture.Create<Product>();
			singleProduct.Id = new ProductId(fixture.Create<int>());

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Product>>();
			mockRepository.Setup(x => x.AddAsync(It.IsAny<Product>(), default))
				.ReturnsAsync(singleProduct);

			var sut = new Create(new CreateValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Result.Should().BeOfType(typeof(CreatedAtRouteResult));
		}


		#endregion

		#region Product Colors

		[Fact]
		public async Task Can_Add_Variant_To_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			// Setup request and entity
			var request = fixture.Create<AddVariantRequest>();
			var product = fixture.Create<Product>();
			product.Id = new ProductId(request.ProductId);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Product>>();
			mockRepository.Setup(x => x.GetByIdAsync(new ProductId(request.ProductId), default))
				.ReturnsAsync(product);

			var sut = new AddVariant(new AddVariantValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(CreatedAtRouteResult));
		}

		[Fact]
		public async Task Can_Update_Variant_In_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			// Setup request and entity
			var request = fixture.Create<UpdateVariantRequest>();
			var product = fixture.Create<Product>();
			product.Id = new ProductId(request.ProductId);

			var variant = fixture.Create<ProductVariant>();
			variant.Sku = request.Sku;

			// Add the variant so we simulate the deletion process
			product.AddVariant(variant);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Product>>();
			mockRepository.Setup(x => x.GetByIdAsync(new ProductId(request.ProductId), default))
				.ReturnsAsync(product);

			var sut = new UpdateVariant(new UpdateVariantValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			product.Variants.First(x => x.Sku == request.Sku).Name.Should().BeEquivalentTo(request.Variant.Name);
			result.Should().BeOfType(typeof(NoContentResult));
		}

		[Fact]
		public async Task Can_Remove_Variant_From_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			// Setup request and entity
			var request = fixture.Create<RemoveVariantRequest>();
			var product = fixture.Create<Product>();
			product.Id = new ProductId(request.ProductId);

			var variant = fixture.Create<ProductVariant>();
			variant.Sku = request.Sku;

			// Add the variant so we simulate the deletion process
			product.AddVariant(variant);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Product>>();
			mockRepository.Setup(x => x.GetByIdAsync(new ProductId(request.ProductId), default))
				.ReturnsAsync(product);

			var sut = new RemoveVariant(new RemoveVariantValidator(), mockRepository.Object);

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		#endregion

		#region Image

		[Fact]
		public async Task Can_Add_Image_To_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<AddImageToVariantRequest>();
			var product = fixture.Create<Product>();
			product.Id = new ProductId(request.ProductId);

			var variant = fixture.Create<ProductVariant>();
			variant.Sku = request.Sku;

			// Add the color so we simulate the addition process
			product.AddVariant(variant);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Product>>();
			mockRepository.Setup(x => x.GetByIdAsync(product.Id, default))
				.ReturnsAsync(product);

			var mockMediator = new Mock<IMediator>();
			mockMediator.Setup(x => x.Send(It.IsAny<UploadImageRequest>(), default))
				.ReturnsAsync(fixture.Create<string>());

			var sut = new AddImage(new AddImageValidator(), mockRepository.Object, fixture.Create<IMapper>(), mockMediator.Object);

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(CreatedAtRouteResult));
		}

		[Fact]
		public async Task Can_Update_Image_In_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());
			fixture.Customize(new MultilanguageStringCustomization());

			var request = fixture.Create<UpdateImageOfVariantRequest>();
			var product = fixture.Create<Product>();
			product.Id = new ProductId(request.ProductId);

			var variant = fixture.Create<ProductVariant>();
			variant.Sku = request.Sku;


			var image = new Image(request.ImagePath, fixture.Create<MultilanguageString>(), false);

			variant.Color = variant.Color.AddImage(image);
			product.AddVariant(variant);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Product>>();
			mockRepository.Setup(x => x.GetByIdAsync(product.Id, default))
				.ReturnsAsync(product);

			var mockMediator = new Mock<IMediator>();
			mockMediator.Setup(x => x.Send(It.IsAny<UploadImageRequest>(), default))
				.ReturnsAsync(fixture.Create<string>());

			var sut = new UpdateImage(new UpdateImageValidator(), mockRepository.Object, mockMediator.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		[Fact]
		public async Task Can_Remove_Image_From_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());
			fixture.Customize(new MultilanguageStringCustomization());

			var request = fixture.Create<RemoveImageFromVariantRequest>();
			var product = fixture.Create<Product>();
			product.Id = new ProductId(request.ProductId);

			var image = new Image(request.ImagePath, fixture.Create<MultilanguageString>(), false);

			var variant = fixture.Create<ProductVariant>();
			variant.Sku = request.Sku;

			variant.Color = variant.Color.AddImage(image);
			product.AddVariant(variant);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Product>>();
			mockRepository.Setup(x => x.GetByIdAsync(product.Id, default))
				.ReturnsAsync(product);

			var sut = new RemoveImage(new RemoveImageValidator(), mockRepository.Object);

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		#endregion
	}
}