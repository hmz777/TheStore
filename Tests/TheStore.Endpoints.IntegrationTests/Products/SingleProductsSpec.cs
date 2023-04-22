using AutoFixture;
using FluentAssertions;
using TheStore.Endpoints.IntegrationTests.WebApplication;
using TheStore.Endpoints.UnitTests.AutoData.Dtos;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Endpoints.IntegrationTests.Products
{
	public class SingleProductsSpec : IClassFixture<CustomWebApplicationFactory<Program>>
	{
		private readonly HttpClient _client;

		public SingleProductsSpec(CustomWebApplicationFactory<Program> factory)
				=> _client = factory.CreateClient();

		[Fact]
		public async Task Can_List_Single_Products()
		{
			var request = new ListRequest(1, 10);

			var response = await _client
				.GetFromJsonAsync<List<SingleProductDto>>(request.Route);

			response.Should().NotBeNull();
			response.Should().HaveCount(request.Take);
		}

		[Fact]
		public async Task Can_Get_Single_Product_By_Id()
		{
			var request = new GetByIdRequest(1);

			var response = await _client
				.GetFromJsonAsync<SingleProductDto>(request.Route);

			response.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Create_Single_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());
			var request = fixture.Create<CreateRequest>();

			var response = await _client
				.PostAsJsonAsync(request.Route, request);

			((int)response.StatusCode).Should().Be(StatusCodes.Status201Created);
			response.Headers.Location.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Update_Single_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());
			var request = fixture.Create<UpdateRequest>();
			request.ProductId = 1;

			var response = await _client
				.PutAsJsonAsync(request.Route, request);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}

		[Fact]
		public async Task Can_Delete_Single_Product()
		{
			var request = new DeleteRequest(20);

			var response = await _client.DeleteAsync(request.Route);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}
	}
}