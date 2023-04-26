using AutoFixture;
using FluentAssertions;
using TheStore.Catalog.Endpoints.IntegrationTests.WebApplication;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Catalog.Endpoints.IntegrationTests.Branches
{
	public class BranchesSpec : IClassFixture<CustomWebApplicationFactory<Program>>
	{
		private readonly HttpClient _client;

		public BranchesSpec(CustomWebApplicationFactory<Program> factory)
				=> _client = factory.CreateClient();

		[Fact]
		public async Task Can_List_Branches()
		{
			var request = new ListRequest(1, 10);

			var response = await _client
				.GetFromJsonAsync<List<BranchDto>>(request.Route);

			response.Should().NotBeNull();
			response.Should().HaveCount(request.Take);
		}

		[Fact]
		public async Task Can_Get_Branch_By_Id()
		{
			var request = new GetByIdRequest(1);

			var response = await _client
				.GetFromJsonAsync<BranchDto>(request.Route);

			response.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Create_Branch()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());
			var request = fixture.Create<CreateRequest>();

			var response = await _client
				.PostAsJsonAsync(CreateRequest.RouteTemplate, request);

			((int)response.StatusCode).Should().Be(StatusCodes.Status201Created);
			response.Headers.Location.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Update_Branch()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());
			var request = fixture.Create<UpdateRequest>();
			request.BranchId = 1;

			var response = await _client
				.PutAsJsonAsync(request.Route, request);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}

		[Fact]
		public async Task Can_Delete_Branch()
		{
			var request = new DeleteRequest(20);

			var response = await _client.DeleteAsync(request.Route);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}
	}
}