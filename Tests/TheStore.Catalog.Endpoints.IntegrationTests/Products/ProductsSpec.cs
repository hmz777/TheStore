using AutoFixture;
using FluentAssertions;
using NCrunch.Framework;
using System.Text.Json;
using TheStore.Catalog.Endpoints.IntegrationTests.WebApplication;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.Endpoints.IntegrationTests.Products
{
	[Atomic]
	public class ProductsSpec : IClassFixture<CustomWebApplicationFactory<Program>>
	{
		private readonly HttpClient _client;

		public ProductsSpec(CustomWebApplicationFactory<Program> factory)
				=> _client = factory.CreateClient();

		[Fact]
		public async Task Can_List_Products()
		{
			var request = new ListRequest(1, 10);

			var response = await _client
				.GetFromJsonAsync<List<ProductDto>>(request.Route);

			response.Should().NotBeNull();
			response.Should().HaveCount(request.Take);
		}

		[Fact]
		public async Task Can_Get_Product_By_Id()
		{
			var request = new GetByIdRequest(1);

			var response = await _client
				.GetFromJsonAsync<ProductDto>(request.Route);

			response.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Create_Product()
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
		public async Task Can_Update_Product()
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
		public async Task Can_Delete_Product()
		{
			var request = new DeleteRequest(20);

			var response = await _client.DeleteAsync(request.Route);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}

		[Fact]
		public async Task Can_Add_Colors_To_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<AddColorRequest>();
			request.ProductId = 1;

			var response = await _client.PostAsJsonAsync(request.Route, request);
			var responseObject = JsonSerializer
				.Deserialize<ProductDto>(
				await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			((int)response.StatusCode).Should().Be(StatusCodes.Status201Created);
			response.Headers.Location.Should().NotBeNull();

			responseObject?.ProductColors.Should().Contain(x => x.ColorCode == request.Color.ColorCode);
		}

		[Fact]
		public async Task Can_Update_Color_In_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<UpdateColorRequest>();
			request.ProductId = 2;
			request.ColorCode = "000000";

			var response = await _client.PutAsJsonAsync(request.Route, request);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}

		[Fact]
		public async Task Can_Remove_Color_From_Product()
		{
			var request = new RemoveColorRequest(3, "000000");

			var response = await _client.DeleteAsync(request.Route);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}

		[Fact]
		public async Task Can_Add_Image_To_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<AddImageToColorRequest>();
			request.ProductId = 4;
			request.ColorCode = "000000";

			using (var formData = new MultipartFormDataContent())
			{
				formData.Add(new StringContent(request.Image.Alt), "Image.Alt");
				formData.Add(new StreamContent(File.OpenRead("TestResources/TestImages/TestImage.jpg")),
					"Image.File", "TestImage.jpg");

				var response = await _client.PostAsync(request.Route, formData);

				var responseObject = JsonSerializer
					.Deserialize<ProductDto>(
					await response.Content.ReadAsStringAsync(),
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				((int)response.StatusCode).Should().Be(StatusCodes.Status201Created);
				response.Headers.Location.Should().NotBeNull();

				responseObject?.ProductColors.Should().Contain(x => x.ColorCode == request.ColorCode);
			}
		}

		[Fact]
		public async Task Can_Update_Image_In_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());

			//var listRequest = new ListRequest(1, 1);
			//var listResponse = await _client.
			//	GetFromJsonAsync<List<ProductDto>>(listRequest.Route);

			//var product = listResponse!.First();
			//var color = product.GetMainColor();

			var request = fixture.Create<UpdateImageOfColorRequest>();
			//request.ProductId = product.ProductId;
			//request.ColorCode = color.ColorCode;
			//request.ImagePath = color.GetMainImage().StringFileUri;
			request.ProductId = 4;
			request.ColorCode = "000000";
			request.ImagePath = "file.png";

			using (var formData = new MultipartFormDataContent())
			{
				formData.Add(new StringContent(request.Image.Alt), "Image.Alt");
				formData.Add(new StreamContent(File.OpenRead("TestResources/TestImages/TestImage.jpg")),
					"Image.File", "TestImage.jpg");

				var response = await _client.PutAsync(request.Route, formData);

				((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
			}
		}

		[Fact]
		public async Task Can_Remove_Image_From_Color()
		{
			var request = new ListRequest(1, 10);

			var response = await _client.
				GetFromJsonAsync<List<ProductDto>>(request.Route);

			var imageToRemove = response!.First();
			var color = imageToRemove.GetMainColor();

			var removeRequest = new RemoveImageFromColorRequest(
				imageToRemove.ProductId,
				color.ColorCode,
				color.GetMainImage().StringFileUri);

			var removeResponse = await _client.DeleteAsync(removeRequest.Route);

			((int)removeResponse.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}
	}
}