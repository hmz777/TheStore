﻿using Ardalis.Specification;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheStore.ApiCommon.Data.Repository;
using TheStore.Catalog.API.Endpoints.SingleProducts;
using TheStore.Catalog.API.Endpoints.SingleProducts.Colors;
using TheStore.Catalog.API.Endpoints.SingleProducts.Colors.Images;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Endpoints;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Services;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.Endpoints.UnitTests.Products
{
	public class SingleProductsSpec
	{
		#region Single Products

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


		#endregion

		#region Product Colors

		[Fact]
		public async Task Can_Add_Color_To_Single_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			// Setup request and entity
			var request = fixture.Create<AddColorRequest>();
			var singleProduct = fixture.Create<SingleProduct>();
			singleProduct.Id = new ProductId(request.ProductId);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, SingleProduct>>();
			mockRepository.Setup(x => x.GetByIdAsync(new ProductId(request.ProductId), default))
				.ReturnsAsync(singleProduct);

			var sut = new AddColor(new AddColorValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(CreatedAtRouteResult));
		}

		[Fact]
		public async Task Can_Update_Single_Product_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			// Setup request and entity
			var request = fixture.Create<UpdateColorRequest>();
			var singleProduct = fixture.Create<SingleProduct>();
			singleProduct.Id = new ProductId(request.ProductId);

			var productColor = new ProductColor(request.ColorCode, new List<Image>());

			// Add the color so we simulate the update process
			singleProduct.AddColor(productColor);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, SingleProduct>>();
			mockRepository.Setup(x => x.GetByIdAsync(new ProductId(request.ProductId), default))
				.ReturnsAsync(singleProduct);

			var sut = new UpdateColor(new UpdateColorsValidator(), mockRepository.Object);

			var result = await sut.HandleAsync(request);

			singleProduct.ProductColors.First(x => x.ColorCode == request.ColorCode).ColorCode.Should().Be(productColor.ColorCode);
			result.Should().BeOfType(typeof(NoContentResult));
		}

		[Fact]
		public async Task Can_Remove_Color_From_Single_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			var mapper = fixture.Create<IMapper>();

			// Setup request and entity
			var request = fixture.Create<RemoveColorRequest>();
			var singleProduct = fixture.Create<SingleProduct>();
			singleProduct.Id = new ProductId(request.ProductId);

			var productColor = new ProductColor(request.ColorCode, new List<Image>());

			// Add the color so we simulate the deletion process
			singleProduct.AddColor(productColor);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, SingleProduct>>();
			mockRepository.Setup(x => x.GetByIdAsync(new ProductId(request.ProductId), default))
				.ReturnsAsync(singleProduct);

			var sut = new RemoveColor(new RemoveColorValidator(), mockRepository.Object);

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

			var request = fixture.Create<AddImageToColorRequest>();
			var singleProduct = fixture.Create<SingleProduct>();
			singleProduct.Id = new ProductId(request.ProductId);

			var productColor = new ProductColor(request.ColorCode, new List<Image>());

			// Add the color so we simulate the addition process
			singleProduct.AddColor(productColor);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, SingleProduct>>();
			mockRepository.Setup(x => x.GetByIdAsync(singleProduct.Id, default))
				.ReturnsAsync(singleProduct);

			var mediator = new Mock<IMediator>();

			var sut = new AddImage(new AddImageValidator(), mockRepository.Object, fixture.Create<IMapper>(), mediator.Object);

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(CreatedAtRouteResult));
		}

		[Fact]
		public async Task Can_Update_Image_In_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<UpdateImageOfColorRequest>();
			var singleProduct = fixture.Create<SingleProduct>();
			singleProduct.Id = new ProductId(request.ProductId);

			var productColor = new ProductColor(request.ColorCode, new List<Image>());

			var image = new Image(request.ImagePath, fixture.Create<string>());

			productColor = productColor.AddImage(image);

			// Add the color so we simulate the addition process
			singleProduct.AddColor(productColor);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, SingleProduct>>();
			mockRepository.Setup(x => x.GetByIdAsync(singleProduct.Id, default))
				.ReturnsAsync(singleProduct);

			var mediator = new Mock<IMediator>();

			var sut = new UpdateImage(new UpdateImageValidator(), mockRepository.Object, fixture.Create<IMapper>(), mediator.Object);

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		[Fact]
		public async Task Can_Remove_Image_From_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new EndpointsCustomization());
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<RemoveImageFromColorRequest>();
			var singleProduct = fixture.Create<SingleProduct>();
			singleProduct.Id = new ProductId(request.ProductId);

			var productColor = new ProductColor(request.ColorCode, new List<Image>());

			var image = new Image(request.ImagePath, fixture.Create<string>());

			productColor = productColor.AddImage(image);

			// Add the color so we simulate the addition process
			singleProduct.AddColor(productColor);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, SingleProduct>>();
			mockRepository.Setup(x => x.GetByIdAsync(singleProduct.Id, default))
				.ReturnsAsync(singleProduct);

			var sut = new RemoveImage(new RemoveImageValidator(), mockRepository.Object);

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		#endregion
	}
}