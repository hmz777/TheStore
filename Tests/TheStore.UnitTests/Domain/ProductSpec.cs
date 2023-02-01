using AutoFixture;
using FluentAssertions;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.Tests.Domain.AutoData.Customizations;

namespace TheStore.Tests.Domain
{
	public class ProductSpec
	{
		[Theory]
		[InlineData(null, null)]
		public void Cant_Create_Valid_Product_Attribute(string name, string description)
		{
			var action = () => new ProductAttribute(name, description);

			action.Should().Throw<ArgumentException>();
		}

		[Theory]
		[InlineData("Some name", "Some description")]
		public void Can_Create_Valid_Product_Attribute(string name, string description)
		{
			var action = () => new ProductAttribute(name, description);

			action.Should().NotThrow();
		}

		[Theory]
		[InlineData("notAHexColor")]
		public void Cant_Create_Valid_Product_Color(string colorCode)
		{
			var action = () => new ProductColor(colorCode, new List<Image>());

			action.Should().Throw<ArgumentException>();
		}

		[Theory]
		[InlineData("#000000")]
		public void Can_Create_Valid_Product_Color(string colorCode)
		{
			var action = () => new ProductColor(colorCode, new List<Image>());

			action.Should().NotThrow();
		}

		[Fact]
		public void Can_Add_Images_To_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new ProductColorCustomization());

			var sut = fixture.Create<ProductColor>();
			var image = fixture.Create<Image>();

			sut = sut.AddImage(image);

			sut.Images.Should().Contain(image);
		}
	}
}