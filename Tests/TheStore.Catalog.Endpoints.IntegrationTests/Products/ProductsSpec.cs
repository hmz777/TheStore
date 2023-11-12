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
				.GetFromJsonAsync<List<ProductDtoRead>>(request.Route);

			response.Should().NotBeNull();
			response.Should().HaveCount(request.Take);
		}

		[Fact]
		public async Task Can_Get_Product_By_Id()
		{
			var request = new GetByIdRequest(1);

			var response = await _client
				.GetFromJsonAsync<ProductDtoRead>(request.Route);

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
		public async Task Can_Add_Variant_To_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<AddVariantRequest>();
			request.ProductId = 1;

			var response = await _client.PostAsJsonAsync(request.Route, request);
			var responseObject = JsonSerializer.Deserialize<ProductDtoRead>(
				await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			((int)response.StatusCode).Should().Be(StatusCodes.Status201Created);
			response.Headers.Location.Should().NotBeNull();

			responseObject?.Variants.Should().Contain(x => x.Name == request.ProductVariant.Name);
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

			var request = fixture.Create<AddImageToVariantRequest>();
			request.ProductId = 4;

			using (var formData = new MultipartFormDataContent())
			{
				formData.Add(new StringContent(JsonSerializer.Serialize(request.Image.Alt)), "Image.Alt");
				formData.Add(new StreamContent(File.OpenRead("TestResources/TestImages/TestImage.jpg")),
					"Image.File", "TestImage.jpg");

				var response = await _client.PostAsync(request.Route, formData);

				var responseObject = JsonSerializer.Deserialize<ProductDtoRead>(
					await response.Content.ReadAsStringAsync(),
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				((int)response.StatusCode).Should().Be(StatusCodes.Status201Created);
				response.Headers.Location.Should().NotBeNull();

				responseObject?.Variants.Where(p => p.Sku == request.Sku).First().Color.Should();
			}
		}

		[Fact]
		public async Task Can_Update_Image_In_Variant()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());

			var listRequest = new ListRequest(1, 1);
			var listResponse = await _client.
				GetFromJsonAsync<List<ProductDtoRead>>(listRequest.Route);

			var product = listResponse!.First();

			var request = fixture.Create<UpdateImageOfVariantRequest>();
			request.ProductId = product.ProductId;
			request.Sku = product.Variants[0].Sku;
			request.ImagePath = product.Variants.First().Color.GetMainImage().StringFileUri;

			using (var formData = new MultipartFormDataContent())
			{
				formData.Add(new StringContent(JsonSerializer.Serialize(request.Image.Alt)), "Image.Alt");
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
				GetFromJsonAsync<List<ProductDtoRead>>(request.Route);

			var product = response!.First();
			var color = product.Variants[0].Color;

			var removeRequest = new RemoveImageFromVariantRequest(
				product.ProductId,
				color.ColorCode,
				color.GetMainImage().StringFileUri);

			var removeResponse = await _client.DeleteAsync(removeRequest.Route);

			((int)removeResponse.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}
	}
}