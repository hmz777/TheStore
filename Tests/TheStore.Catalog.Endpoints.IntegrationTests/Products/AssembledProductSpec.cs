using AutoFixture;
using FluentAssertions;
using TheStore.Catalog.Endpoints.IntegrationTests.WebApplication;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.Endpoints.IntegrationTests.Products
{
	public class AssembledProductSpec : IClassFixture<CustomWebApplicationFactory<Program>>
	{
		private readonly HttpClient _client;

		public AssembledProductSpec(CustomWebApplicationFactory<Program> factory)
				=> _client = factory.CreateClient();

		public async Task Can_List_Assembled_Products()
		{
			var request = new ListAssembledRequest(1, 10);

			var response = await _client
				.GetFromJsonAsync<List<AssembledProductDtoRead>>(request.Route);

			response.Should().NotBeNull();
			response.Should().HaveCount(request.Take);
		}

		public async Task Can_Get_Assembled_Product_By_Id()
		{
			var request = new GetAssembledByIdRequest(1);

			var response = await _client
				.GetFromJsonAsync<AssembledProductDtoRead>(request.Route);

			response.Should().NotBeNull();
		}

		public async Task Can_Create_Assembled_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());
			var request = fixture.Create<CreateAssembledRequest>();

			var response = await _client
				.PostAsJsonAsync(request.Route, request);

			((int)response.StatusCode).Should().Be(StatusCodes.Status201Created);
			response.Headers.Location.Should().NotBeNull();
		}

		public async Task Can_Update_Assembled_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());
			var request = fixture.Create<UpdateAssembledRequest>();
			request.ProductId = 1;

			var response = await _client
				.PutAsJsonAsync(request.Route, request);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}

		public async Task Can_Delete_Assembled_Product()
		{
			var request = new DeleteAssembledRequest(20);

			var response = await _client.DeleteAsync(request.Route);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}

		public async Task Can_Add_Parts_To_Assembled_Product()
		{
			var fixture = new Fixture();
			var request = fixture.Create<AddPartRequest>();

			var response = await _client
				.PostAsJsonAsync(request.Route, request);

			((int)response.StatusCode).Should().Be(StatusCodes.Status201Created);
			response.Headers.Location.Should().NotBeNull();
		}

		public async Task Can_Remove_Parts_To_Assembled_Product()
		{
			var fixture = new Fixture();
			var request = fixture.Create<RemovePartRequest>();

			var response = await _client
				.PostAsJsonAsync(request.Route, request);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}
	}
}