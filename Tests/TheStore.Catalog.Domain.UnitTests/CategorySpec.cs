using AutoFixture;
using FluentAssertions;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Domain.UnitTests.AutoData.Customizations;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Domain.UnitTests
{
	public class CategorySpec
	{
		[Theory]
		[InlineData(-1, "name")]
		[InlineData(0, "name")]
		[InlineData(1, null)]
		public void Cant_Create_Invalid_Category(int order, string name)
		{
			var action = () => new Category(order, new MultilanguageString(name, CultureCode.English), false);

			action.Should().Throw<Exception>();
		}

		[Fact]
		public void Can_Create_Valid_Category()
		{
			var fixture = new Fixture();
			fixture.Customize(new MultilanguageStringCustomization());

			var action = () => new Category(1, fixture.Create<MultilanguageString>(), false);

			action.Should().NotThrow<Exception>();
		}
	}
}
