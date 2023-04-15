using Ardalis.Specification;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using TheStore.ApiCommon.Data.Repository;
using TheStore.Catalog.API.Endpoints.Categories;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Endpoints.Tests.AutoData.DomainCustomizations;
using TheStore.Endpoints.Tests.AutoData.ServiceCustomizations;
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
			fixture.Customize(new DomainCustomization());

			var request = new ListRequest(1, 10);

			var mockRepository = new Mock<IReadApiRepository<CatalogDbContext, Category>>();
			mockRepository.Setup(x => x.ListAsync(It.IsAny<Specification<Category>>(), default))
				.ReturnsAsync(fixture.CreateMany<Category>(request.Take).ToList());

			var sut = new List(new ListValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var categoryDtos = (await sut.HandleAsync(request)).Value;

			categoryDtos.Should().HaveCount(request.Take);
		}
	}
}