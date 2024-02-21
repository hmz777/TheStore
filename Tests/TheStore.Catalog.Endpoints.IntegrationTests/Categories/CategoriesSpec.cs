using AutoFixture;
using FluentAssertions;
using NCrunch.Framework;
using TheStore.Catalog.Endpoints.IntegrationTests.WebApplication;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos;
using TheStore.SharedModels.Models.Categories;

namespace TheStore.Catalog.Endpoints.IntegrationTests.Categories
{
	[Atomic]
	public class CategoriesSpec : IClassFixture<CustomWebApplicationFactory<Program>>
	{
		private readonly HttpClient _client;

		public CategoriesSpec(CustomWebApplicationFactory<Program> factory)
				=> _client = factory.CreateClient();

		[Fact]
		public async Task Can_List_Categories()
		{
			var request = new ListRequest() { Page = 1, Take = 10 };

			var response = await _client
				.GetFromJsonAsync<List<CategoryDtoRead>>(request.Route);

			response.Should().NotBeNull();
			response.Should().HaveCount(request.Take);
		}

		[Fact]
		public async Task Can_Get_Category_By_Id()
		{
			var request = new GetByIdRequest() { CategoryId = 1 };

			var response = await _client
				.GetFromJsonAsync<CategoryDtoRead>(request.Route);

			response.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Create_Category()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());
			var request = fixture.Create<CreateRequest>();

			var response = await _client
				.PostAsJsonAsync(request.Route, request.Category);

			((int)response.StatusCode).Should().Be(StatusCodes.Status201Created);
			response.Headers.Location.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Update_Category()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());
			var request = fixture.Create<UpdateRequest>();
			request.CategoryId = 1;

			var response = await _client
				.PutAsJsonAsync(request.Route, request.Category);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}

		[Fact]
		public async Task Can_Delete_Category()
		{
			var request = new DeleteRequest() { CategoryId = 20 };

			var response = await _client.DeleteAsync(request.Route);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}
	}
}