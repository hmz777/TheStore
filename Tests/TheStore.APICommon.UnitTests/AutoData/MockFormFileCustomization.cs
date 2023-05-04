using AutoFixture;
using AutoFixture.Kernel;
using Microsoft.AspNetCore.Http;
using System.IO.Abstractions.TestingHelpers;

namespace TheStore.APICommon.UnitTests.AutoData
{
	public class MockFormFileCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customizations.Add(new TypeRelay(typeof(IFormFile), typeof(FormFile)));
			fixture.Register(() =>
			{
				var rand = new Random();
				var mockFileStream = new MockFileStream(new MockFileSystem(), "/", FileMode.Create);
				var formFile = new FormFile(mockFileStream, 0, 500, $"TestFile{rand.Next()}", $"TestFile{rand.Next()}.png");

				return formFile;
			});
		}
	}
}