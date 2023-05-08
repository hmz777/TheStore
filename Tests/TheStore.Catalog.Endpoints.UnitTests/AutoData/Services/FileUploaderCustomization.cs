using AutoFixture;
using AutoFixture.Kernel;
using System.IO.Abstractions.TestingHelpers;
using TheStore.ApiCommon.Services;

namespace TheStore.Catalog.Endpoints.UnitTests.AutoData.Services
{
	public class FileUploaderCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customizations.Add(new TypeRelay(typeof(IFileUploader), typeof(FileUploader)));
			fixture.Register(() =>
			{
				var fileSystem = new MockFileSystem();
				var fileUploader = new FileUploader(fileSystem);

				return fileUploader;
			});
		}
	}
}