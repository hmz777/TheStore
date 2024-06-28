using AutoFixture;
using FluentAssertions;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.SharedKernel.ValueObjects;
using TheStore.TestHelpers.AutoData.Customizations;

namespace TheStore.Catalog.Domain.UnitTests
{
	public class ProductSpec
	{
		#region Product Attributes

		[Theory]
		[InlineData(null, null)]
		public void Cant_Create_Invalid_Product_Attribute(string? name, string? description)
		{
			var action = () => new ProductAttribute(name!, description!);

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
		[InlineData(null, "notAHexColor")]
		[InlineData(null, "#000000")]
		[InlineData("Black", null)]
		public void Cant_Create_Invalid_Product_Color(string? colorName, string? colorCode)
		{
			var action = () => new ProductColor(colorName, colorCode!, false, []);

			action.Should().Throw<ArgumentException>();
		}

		[Fact]
		public void Can_Create_Valid_Product_Color()
		{
			var action = () => new ProductColor("Black", "000000", false, []);

			action.Should().NotThrow();
		}

		[Fact]
		public void Can_Add_Images_To_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());
			var image = fixture.Create<Image>();

			var sut = new ProductColor("Black", "000000", false, []);
			sut.Images.Add(image);

			sut.Images.Should().Contain(image);
		}

		[Fact]
		public void Can_Remove_Images_From_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());
			var image = fixture.Create<Image>();

			var sut = new ProductColor("Black", "000000", false, []);

			sut.Images.Add(image);
			sut.Images.Remove(image);

			sut.Images.Should().NotContain(image);
		}

		#endregion

		#region Product

		[Theory]
		[InlineData(0, null, "qdqw-dqwd-qwdq")]
		[InlineData(1, null, "qwdqw-dqwd-qwd")]
		[InlineData(0, "name", "qdqwdqw-qwdqwd")]
		[InlineData(0, "name", "qwdqw?@!@#")]
		public void Cant_Create_Valid_Product(int categoryId, string? name, string? identifier)
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());

			var action = () => new Product(
				new CategoryId(categoryId),
				name!,
				identifier!,
				fixture.Create<MultilanguageString>(),
				fixture.Create<MultilanguageString>(),
				false,
				fixture.CreateMany<ProductVariant>().ToList());

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
		public void Can_Add_Variant_To_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());
			var variant = fixture.Create<ProductVariant>();

			var sut = fixture.Create<Product>();

			sut.Variants.Add(variant);

			sut.Variants.Should().Contain(variant);
		}

		[Fact]
		public void Can_Remove_Variant_From_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());
			var variant = fixture.Create<ProductVariant>();

			var sut = fixture.Create<Product>();

			sut.Variants.Remove(variant);

			sut.Variants.Should().NotContain(variant);
		}

		#endregion

		#region Assembled Product

		[Fact]
		public void Can_Add_Parts_To_Assembled_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());
			var partId = new ProductId(1);
			var variantSku = fixture.Create<string>();

			var sut = fixture.Create<AssembledProduct>();

			sut.AddPart(partId, variantSku);

			sut.Parts.Should().Contain(KeyValuePair.Create(partId, variantSku));
		}

		[Fact]
		public void Can_Remove_Parts_From_Assembled_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());
			var partId = new ProductId(1);
			var variantSku = fixture.Create<string>();

			var sut = fixture.Create<AssembledProduct>();
			sut.AddPart(partId, variantSku);

			var result = sut.RemovePart(partId);

			result.Should().Be(true);
			sut.Parts.Should().NotContain(KeyValuePair.Create(partId, variantSku));
		}

		#endregion
	}
}