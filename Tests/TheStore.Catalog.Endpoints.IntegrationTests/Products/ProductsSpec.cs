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
			var request = new ListRequest() { Page = 1, Take = 10 };

			var response = await _client
				.GetFromJsonAsync<List<ProductDtoRead>>(request.Route);

			response.Should().NotBeNull();
			response.Should().HaveCount(request.Take);
		}

		[Fact]
		public async Task Can_Get_Product_By_Id()
		{
			var request = new GetByIdRequest() { ProductId = 1 };

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
				.PostAsJsonAsync(request.Route, request.Product);

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
				.PutAsJsonAsync(request.Route, request.Product);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}

		[Fact]
		public async Task Can_Delete_Product()
		{
			var request = new DeleteRequest { ProductId = 1 };

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

			var response = await _client.PostAsJsonAsync(request.Route, request.ProductVariant);
			var responseObject = JsonSerializer.Deserialize<ProductDtoRead>(
				await response.Content.ReadAsStringAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			((int)response.StatusCode).Should().Be(StatusCodes.Status201Created);
			response.Headers.Location.Should().NotBeNull();

			responseObject?.Variants.Should().Contain(x => x.Name == request.ProductVariant.Name);
		}

		[Fact]
		public async Task Can_Add_Image_To_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());

			var request = fixture.Create<AddImageToVariantRequest>();
			request.ProductId = 1;
			request.Sku = "SKU 0";

			using (var formData = new MultipartFormDataContent())
			{
				formData.Add(new StringContent("en-US"), $"{nameof(request.Image)}.{nameof(request.Image.Alt)}.LocalizedStrings[0].CultureCode.Code");
				formData.Add(new StringContent("Lorem Ipsum"), $"{nameof(request.Image)}.{nameof(request.Image.Alt)}.LocalizedStrings[0].Value");
				formData.Add(new StringContent("True"), $"{nameof(request.Image)}.{nameof(request.Image.IsMainImage)}");
				formData.Add(new StreamContent(request.Image.File.OpenReadStream()), $"{nameof(request.Image)}.{nameof(request.Image.File)}", request.Image.File.FileName);

				var response = await _client.PostAsync(request.Route, formData);

				((int)response.StatusCode).Should().Be(StatusCodes.Status201Created);
				response.Headers.Location.Should().NotBeNull();
			}
		}

		[Fact]
		public async Task Can_Update_Image_In_Variant()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());

			var listRequest = new ListRequest() { Page = 1, Take = 1 };
			var listResponse = await _client.
				GetFromJsonAsync<List<ProductDtoRead>>(listRequest.Route);

			var product = listResponse!.First();

			var request = fixture.Create<UpdateImageOfVariantRequest>();
			request.ProductId = product.ProductId;
			request.Sku = product.Variants[0].Sku;
			request.ImagePath = product.Variants.First().Color.GetMainImage().StringFileUri;

			using (var formData = new MultipartFormDataContent())
			{
				formData.Add(new StringContent("en-US"), $"{nameof(request.Image)}.{nameof(request.Image.Alt)}.LocalizedStrings[0].CultureCode.Code");
				formData.Add(new StringContent("Lorem Ipsum"), $"{nameof(request.Image)}.{nameof(request.Image.Alt)}.LocalizedStrings[0].Value");
				formData.Add(new StringContent("True"), $"{nameof(request.Image)}.{nameof(request.Image.IsMainImage)}");
				formData.Add(new StreamContent(request.Image.File.OpenReadStream()), $"{nameof(request.Image)}.{nameof(request.Image.File)}", request.Image.File.FileName);

				var response = await _client.PutAsync(request.Route, formData);

				((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
			}
		}

		[Fact]
		public async Task Can_Remove_Image_From_Color()
		{
			var request = new ListRequest() { Page = 1, Take = 10 };

			var response = await _client.
				GetFromJsonAsync<List<ProductDtoRead>>(request.Route);

			var product = response!.First();

			var removeRequest = new RemoveImageFromVariantRequest
			{
				ProductId = product.ProductId,
				Sku = product.Variants[0].Sku,
				ImagePath = product.Variants.First().Color.GetMainImage().StringFileUri
			};

			var removeResponse = await _client.DeleteAsync(removeRequest.Route);

			((int)removeResponse.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}
	}
}