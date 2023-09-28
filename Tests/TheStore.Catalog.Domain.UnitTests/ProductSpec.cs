using AutoFixture;
using FluentAssertions;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.Catalog.Domain.UnitTests.AutoData.Customizations;

namespace TheStore.Catalog.Domain.UnitTests
{
	public class ProductSpec
	{
		#region Product Attributes

		[Theory]
		[InlineData(null, null)]
		public void Cant_Create_Invalid_Product_Attribute(string name, string description)
		{
			var action = () => new ProductAttribute(name, description);

			action.Should().Throw<ArgumentException>();
		}

		[Fact]
		public void Can_Create_Valid_Product_Attribute()
		{
			var action = () => new ProductAttribute("name", "description");

			action.Should().NotThrow();
		}

		#endregion

		#region Product Color

		[Theory]
		[InlineData("notAHexColor")]
		public void Cant_Create_Invalid_Product_Color(string colorCode)
		{
			var action = () => new ProductColor(colorCode, false, InventoryRecord.Empty, new List<Image>());

			action.Should().Throw<ArgumentException>();
		}

		[Fact]
		public void Can_Create_Valid_Product_Color()
		{
			var action = () => new ProductColor("000000", false, InventoryRecord.Empty, new List<Image>());

			action.Should().NotThrow();
		}

		[Fact]
		public void Can_Add_Images_To_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());
			var image = fixture.Create<Image>();

			var sut = new ProductColor("000000", false, InventoryRecord.Empty, new List<Image>());

			sut = sut.AddImage(image);

			sut.Images.Should().Contain(image);
		}

		[Fact]
		public void Can_Remove_Images_From_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());
			var image = fixture.Create<Image>();

			var sut = new ProductColor("000000", false, InventoryRecord.Empty, new List<Image>());

			sut = sut.AddImage(image);
			sut = sut.RemoveImage(image);

			sut.Images.Should().NotContain(image);
		}

		#endregion

		#region Product

		[Theory]
		[InlineData(0, null, "desc", "sdesc", "sku")]
		[InlineData(1, null, "desc", "sdesc", "sku")]
		[InlineData(1, "name", null, "sdesc", "sku")]
		[InlineData(1, "name", "desc", null, "sku")]
		[InlineData(1, "name", "desc", "sdesc", null)]
		public void Cant_Create_Valid_Product(int categoryId, string name, string description, string shortDescription, string sku)
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());

			var action = () => new Product(
				new CategoryId(categoryId),
				name, description,
				shortDescription,
				sku,
				fixture.Create<Money>(),
				fixture.CreateMany<ProductColor>().ToList());

			action.Should().Throw<Exception>();
		}

		[Fact]
		public void Can_Create_Valid_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());

			var action = () => fixture.Create<Product>();

			action.Should().NotThrow<Exception>();
		}

		[Fact]
		public void Can_Add_Color_To_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());
			var color = fixture.Create<ProductColor>();

			var sut = fixture.Create<Product>();

			sut.AddColor(color);

			sut.ProductColors.Should().Contain(color);
		}

		[Fact]
		public void Can_Remove_Color_To_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());
			var color = fixture.Create<ProductColor>();

			var sut = fixture.Create<Product>();

			sut.RemoveColor(color);

			sut.ProductColors.Should().NotContain(color);
		}

		#endregion

		#region Assembled Product

		[Fact]
		public void Can_Add_Parts_To_Assembled_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());
			var newPartId = new ProductId(1);

			var sut = fixture.Create<AssembledProduct>();

			sut.AddPart(newPartId);

			sut.Parts.Should().Contain(newPartId);
		}

		[Fact]
		public void Can_Remove_Parts_From_Assembled_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());
			var newPartId = new ProductId(1);

			var sut = fixture.Create<AssembledProduct>();
			sut.AddPart(newPartId);

			var result = sut.RemovePart(newPartId);

			result.Should().Be(true);
			sut.Parts.Should().NotContain(newPartId);
		}

		#endregion
	}
}