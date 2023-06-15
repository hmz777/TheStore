using FluentAssertions;
using TheStore.Catalog.Core.ValueObjects;

namespace TheStore.Catalog.Domain.UnitTests
{
	public class ImageSpec
	{
		[Theory]
		[InlineData("file://someImage", "Image alt")]
		[InlineData("file://someImage.txt", "Image alt")]
		[InlineData("http://www.imageuri.com/image.png", "Image alt")]
		[InlineData("http://www.imageuri.com/image", "Image alt")]
		public void Should_Create_Valid_Image(string uri, string alt)
		{
			var action = () => new Image(uri, alt);

			action.Should().NotThrow<Exception>();
		}
	}
}