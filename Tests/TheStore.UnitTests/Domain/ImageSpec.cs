using FluentAssertions;
using TheStore.Catalog.Core.ValueObjects;

namespace TheStore.Tests.Domain
{
    public class ImageSpec
    {
        [Theory]
        [InlineData("not_a_file_uri", "Image alt")]
        [InlineData("https://www.siteNoExtension.com/im", "qwdqwdqwk")]
        public void Shouldnt_Create_Valid_Image(string uri, string alt)
        {
            var action = () => new Image(new Uri(uri), alt);

            action.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData("file://someImage", "Image alt")]
        [InlineData("file://someImage.txt", "Image alt")]
        [InlineData("http://www.imageuri.com/image.png", "Image alt")]
        public void Should_Create_Valid_Image(string uri, string alt)
        {
            var action = () => new Image(new Uri(uri), alt);

            action.Should().NotThrow<Exception>();
        }
    }
}