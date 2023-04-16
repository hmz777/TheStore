using Ardalis.Specification;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheStore.ApiCommon.Data.Repository;
using TheStore.Catalog.API.Endpoints.Categories;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Endpoints.Tests.AutoData.Endpoints;
using TheStore.Endpoints.Tests.AutoData.Services;
using TheStore.SharedModels.Models.Categories;

namespace TheStore.Endpoints.Tests.UnitTests.Categories
{
	public class CategoriesSpec
	{
		[Fact]
		public async Task Can_List_Categories()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = new ListRequest(1, 10);

			var mockRepository = new Mock<IReadApiRepository<CatalogDbContext, Category>>();
			mockRepository.Setup(x => x.ListAsync(It.IsAny<Specification<Category>>(), default))
				.ReturnsAsync(fixture.CreateMany<Category>(request.Take).ToList());

			var sut = new List(new ListValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = (await sut.HandleAsync(request)).Value;

			result.Should().HaveCount(request.Take);
		}

		[Fact]
		public async Task Can_Get_Category_By_Id()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = new GetByIdRequest(fixture.Create<int>());
			var category = fixture.Create<Category>();
			category.Id = new CategoryId(request.CategoryId);

			var mockRepository = new Mock<IReadApiRepository<CatalogDbContext, Category>>();
			mockRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Specification<Category>>(), default))
				.ReturnsAsync(category);

			var sut = new GetById(new GetByIdValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = (await sut.HandleAsync(request)).Value;

			result.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Delete_Category()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = new DeleteRequest(fixture.Create<int>());
			var category = fixture.Create<Category>();
			category.Id = new CategoryId(request.CategoryId);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Category>>();
			mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<CategoryId>(), default))
				.ReturnsAsync(category);

			var sut = new Delete(new DeleteValidator(), mockRepository.Object);

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		[Fact]
		public async Task Can_Update_Category()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = new UpdateRequest(fixture.Create<int>(), fixture.Create<int>(), fixture.Create<string>(), false);
			var category = fixture.Create<Category>();
			category.Id = new CategoryId(request.CategoryId);

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Category>>();
			mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<CategoryId>(), default))
				.ReturnsAsync(category);

			var sut = new Update(new UpdateValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		[Fact]
		public async Task Can_Create_Category()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			fixture.Freeze<string>("Category Name");
			fixture.Freeze<int>(1);

			var request = new CreateRequest(fixture.Create<int>(), fixture.Create<string>(), false);
			var category = fixture.Create<Category>();
			category.Id = new CategoryId(fixture.Create<int>());

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Category>>();
			mockRepository.Setup(x => x.AddAsync(It.IsAny<Category>(), default))
				.ReturnsAsync(category);

			var sut = new Create(new CreateValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Result.Should().BeOfType(typeof(CreatedAtRouteResult));
		}
	}
}