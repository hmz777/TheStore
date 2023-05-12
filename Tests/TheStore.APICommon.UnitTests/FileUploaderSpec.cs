using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.IO.Abstractions.TestingHelpers;
using TheStore.ApiCommon.Services;
using TheStore.APICommon.UnitTests.AutoData;

namespace TheStore.APICommon.UnitTests
{
	public class FileUploaderSpec
	{
		[Fact]
		public async Task Can_Successfully_Upload_File()
		{
			var fixture = new Fixture();
			fixture.Customize(new MockFormFileCustomization());
			var file = fixture.Create<IFormFile>();
			var location = "images";

			var fileSystemMock = new MockFileSystem();
			var sut = new FileUploader(fileSystemMock);

			var filePath = await sut.UploadFileAsync(location, file);

			fileSystemMock.AllFiles.Should().HaveCount(1);
			fileSystemMock.AllFiles.Should().Contain(fileSystemMock.Path.Combine("C:\\", filePath));
		}

		[Fact]
		public async Task Can_Successfully_Upload_Files()
		{
			var fixture = new Fixture();
			fixture.Customize(new MockFormFileCustomization());
			var files = fixture.CreateMany<IFormFile>();
			var location = "images";

			var fileSystemMock = new MockFileSystem();
			var sut = new FileUploader(fileSystemMock);

			var filePaths = await sut.UploadFilesAsync(location, files);

			fileSystemMock.AllFiles.Should().HaveCount(files.Count());
			fileSystemMock.AllFiles.Should().Equal(filePaths.Select(f => fileSystemMock.Path.Combine("C:\\", f)));
		}
	}
}