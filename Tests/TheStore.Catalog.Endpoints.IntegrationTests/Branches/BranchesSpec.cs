﻿using AutoFixture;
using FluentAssertions;
using NCrunch.Framework;
using System.Text.Json;
using TheStore.Catalog.Endpoints.IntegrationTests.AutoData;
using TheStore.Catalog.Endpoints.IntegrationTests.WebApplication;
using TheStore.Catalog.Endpoints.UnitTests.AutoData.Dtos;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Catalog.Endpoints.IntegrationTests.Branches
{
	[Atomic]
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
				.GetFromJsonAsync<List<BranchDtoUpdate>>(request.Route);

			response.Should().NotBeNull();
			response.Should().HaveCount(request.Take);
		}

		[Fact]
		public async Task Can_Get_Branch_By_Id()
		{
			var request = new GetByIdRequest(1);

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
				.PostAsJsonAsync(request.Route, request);

			((int)response.StatusCode).Should().Be(StatusCodes.Status201Created);
			response.Headers.Location.Should().NotBeNull();
		}

		[Fact]
		public async Task Can_Update_Branch()
		{
			var fixture = new Fixture();
			fixture.Customize(new DtoCustomizations());
			var request = fixture.Create<UpdateRequest>();
			request.BranchId = 2;

			var response = await _client
				.PutAsJsonAsync(request.Route, request);

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
				formData.Add(new StringContent(request.BranchId.ToString()), nameof(request.BranchId));
				formData.Add(new StringContent(JsonSerializer.Serialize(request.Image.Alt)), $"{nameof(request.Image)}.{nameof(request.Image.Alt)}");
				formData.Add(new StreamContent(request.Image.File.OpenReadStream()), $"{nameof(request.Image)}.{nameof(request.Image.File)}", request.Image.File.FileName);

				var response = await _client
						.PutAsync(request.Route, formData);

				((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
			}
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