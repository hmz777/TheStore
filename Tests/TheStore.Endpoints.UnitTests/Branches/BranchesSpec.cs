using Ardalis.Specification;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheStore.ApiCommon.Data.Repository;
using TheStore.Catalog.API.Endpoints.Branches;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Endpoints.UnitTests.AutoData.Endpoints;
using TheStore.Endpoints.UnitTests.AutoData.Services;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Endpoints.UnitTests.Branches
{
	public class BranchesSpec
	{
		[Fact]
		public async Task Can_List_Branches()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = new ListRequest(1, 10);

			var mockRepository = new Mock<IReadApiRepository<CatalogDbContext, Branch>>();
			mockRepository.Setup(x => x.ListAsync(It.IsAny<Specification<Branch>>(), default))
				.ReturnsAsync(fixture.CreateMany<Branch>(request.Take).ToList());

			var sut = new List(new ListValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = (await sut.HandleAsync(request)).Value;

			result.Should().HaveCount(request.Take);
		}

		[Fact]
		public async Task Can_Get_Branch_By_Id()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = fixture.Create<GetByIdRequest>();
			var branch = fixture.Create<Branch>();
			branch.Id = request.BranchId;

			var mockRepository = new Mock<IReadApiRepository<CatalogDbContext, Branch>>();
			mockRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Specification<Branch>>(), default))
				.ReturnsAsync(branch);

			var sut = new GetById(new GetByIdValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = (await sut.HandleAsync(request)).Value;

			result.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Delete_Branch()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = fixture.Create<DeleteRequest>();
			var branch = fixture.Create<Branch>();
			branch.Id = request.BranchId;

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Branch>>();
			mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default))
				.ReturnsAsync(branch);

			var sut = new Delete(new DeleteValidator(), mockRepository.Object);

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		[Fact]
		public async Task Can_Update_Branch()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = fixture.Create<UpdateRequest>();
			var branch = fixture.Create<Branch>();
			branch.Id = request.BranchId;

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Branch>>();
			mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default))
				.ReturnsAsync(branch);

			var sut = new Update(new UpdateValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Should().BeOfType(typeof(NoContentResult));
		}

		[Fact]
		public async Task Can_Create_Branch()
		{
			var fixture = new Fixture();
			fixture.Customize(new AutoMapperCustomization());
			fixture.Customize(new EndpointsCustomization());

			var request = fixture.Create<CreateRequest>();
			var branch = fixture.Create<Branch>();
			branch.Id = fixture.Create<int>();

			var mockRepository = new Mock<IApiRepository<CatalogDbContext, Branch>>();
			mockRepository.Setup(x => x.AddAsync(It.IsAny<Branch>(), default))
				.ReturnsAsync(branch);

			var sut = new Create(new CreateValidator(), mockRepository.Object, fixture.Create<IMapper>());

			var result = await sut.HandleAsync(request);

			result.Result.Should().BeOfType(typeof(CreatedAtRouteResult));
		}
	}
}