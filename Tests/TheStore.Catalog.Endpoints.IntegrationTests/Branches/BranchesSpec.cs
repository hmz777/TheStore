using AutoFixture;
using FluentAssertions;
using NCrunch.Framework;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Data.Configuration;
using TheStore.Requests.Models.Branches;
using TheStore.SharedModels.Models.Branches;
using TheStore.TestHelpers.AutoData.Customizations;
using TheStore.TestHelpers.WebApplication;

namespace TheStore.Catalog.Endpoints.IntegrationTests.Branches
{
	[Atomic]
	public class BranchesSpec : IClassFixture<CustomWebApplicationFactory<Program, CatalogDbContext>>
	{
		private readonly HttpClient _client;

		public BranchesSpec(CustomWebApplicationFactory<Program, CatalogDbContext> factory)
		{
			factory.DbName = Constants.DatabaseName;
			_client = factory.CreateClient();
		}

		[Fact]
		public async Task Can_List_Branches()
		{
			var request = new ListRequest() { Page = 1, Take = 10 };

			var response = await _client
				.GetFromJsonAsync<List<BranchDtoUpdate>>(request.Route);

			response.Should().NotBeNull();
			response.Should().HaveCount(request.Take);
		}

		[Fact]
		public async Task Can_Get_Branch_By_Id()
		{
			var request = new GetByIdRequest() { BranchId = 1 };

			var response = await _client
				.GetFromJsonAsync<BranchDtoUpdate>(request.Route);

			response.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Create_Branch()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());
			var request = fixture.Create<CreateRequest>();

			var response = await _client
				.PostAsJsonAsync(request.Route, request.Branch);

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
				.PutAsJsonAsync(request.Route, request.Branch);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}

		[Fact]
		public async Task Can_Update_Branch_Image()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());
			fixture.Customize(new FormFileCustomization());
			var request = fixture.Create<UpdateImageRequest>();
			request.BranchId = 2;

			using (var formData = new MultipartFormDataContent())
			{
				formData.Add(new StringContent("en-US"), $"{nameof(request.Image)}.{nameof(request.Image.Alt)}.LocalizedStrings[0].CultureCode.Code");
				formData.Add(new StringContent("Lorem Ipsum"), $"{nameof(request.Image)}.{nameof(request.Image.Alt)}.LocalizedStrings[0].Value");
				formData.Add(new StringContent("True"), $"{nameof(request.Image)}.{nameof(request.Image.IsMainImage)}");
				formData.Add(new StreamContent(request.Image.File.OpenReadStream()), $"{nameof(request.Image)}.{nameof(request.Image.File)}", request.Image.File.Name);

				var response = await _client
						.PutAsync(request.Route, formData);

				((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
			}
		}

		[Fact]
		public async Task Can_Delete_Branch()
		{
			var request = new DeleteRequest() { BranchId = 20 };

			var response = await _client.DeleteAsync(request.Route);

			((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
		}
	}
}