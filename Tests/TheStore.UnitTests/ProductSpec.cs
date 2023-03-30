﻿using AutoFixture;
using FluentAssertions;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.Domain.Tests.AutoData.Customizations;

namespace TheStore.Domain.Tests
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
			var action = () => new ProductColor(colorCode, new List<Image>());

			action.Should().Throw<ArgumentException>();
		}

		[Fact]
		public void Can_Create_Valid_Product_Color()
		{
			var action = () => new ProductColor("#000000", new List<Image>());

			action.Should().NotThrow();
		}

		[Fact]
		public void Can_Add_Images_To_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new ProductColorCustomization());
			var image = fixture.Create<Image>();

			var sut = new ProductColor("#000000", new List<Image>());

			sut = sut.AddImage(image);

			sut.Images.Should().Contain(image);
		}

		[Fact]
		public void Can_Remove_Images_From_Color()
		{
			var fixture = new Fixture();
			fixture.Customize(new ProductColorCustomization());
			var image = fixture.Create<Image>();

			var sut = new ProductColor("#000000", new List<Image>());

			sut = sut.AddImage(image);
			sut = sut.RemoveImage(image);

			sut.Images.Should().NotContain(image);
		}

		#endregion

		#region Product

		[Theory]
		[InlineData(null, "desc", "sdesc", "sku")]
		[InlineData("name", null, "sdesc", "sku")]
		[InlineData("name", "desc", null, "sku")]
		[InlineData("name", "desc", "sdesc", null)]
		public void Cant_Create_Valid_Product(string name, string description, string shortDescription, string sku)
		{
			var fixture = new Fixture();
			fixture.Customize(new InventoryRecordCustomization());

			var action = () => new Product(name, description, shortDescription, sku, fixture.Create<InventoryRecord>());

			action.Should().Throw<Exception>();
		}

		[Fact]
		public void Can_Create_Valid_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new InventoryRecordCustomization());

			var action = () => new Product("sss", "sss", "sss", "sss", fixture.Create<InventoryRecord>());

			action.Should().NotThrow<Exception>();
		}

		[Fact]
		public void Can_Add_Color_To_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new InventoryRecordCustomization());
			fixture.Customize(new ProductColorCustomization());
			var color = fixture.Create<ProductColor>();

			var sut = new Product("sss", "sss", "sss", "sss", fixture.Create<InventoryRecord>());
			sut.AddOrUpdateColor(color);

			sut.ProductColors.Should().Contain(color);
		}

		[Fact]
		public void Can_Remove_Color_To_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new InventoryRecordCustomization());
			fixture.Customize(new ProductColorCustomization());
			var color = fixture.Create<ProductColor>();

			var sut = new Product("sss", "sss", "sss", "sss", fixture.Create<InventoryRecord>());
			sut.RemoveColor(color);

			sut.ProductColors.Should().NotContain(color);
		}

		#endregion

		#region Assembled Product

		[Fact]
		public void Can_Add_Parts_To_Assembled_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new InventoryRecordCustomization());
			var newPartId = new ProductId(1);

			var sut = new AssembledProduct(new List<ProductId>(), new CategoryId(1), "name", "desc", "sdesc", "sku", fixture.Create<InventoryRecord>());

			sut.AddPart(newPartId);

			sut.Parts.Should().Contain(newPartId);
		}

		[Fact]
		public void Can_Remove_Parts_To_Assembled_Product()
		{
			var fixture = new Fixture();
			fixture.Customize(new InventoryRecordCustomization());
			var newPartId = new ProductId(1);

			var sut = new AssembledProduct(new List<ProductId>(), new CategoryId(1), "name", "desc", "sdesc", "sku", fixture.Create<InventoryRecord>());

			sut.RemovePart(newPartId);

			sut.Parts.Should().NotContain(newPartId);
		}

		#endregion
	}
}