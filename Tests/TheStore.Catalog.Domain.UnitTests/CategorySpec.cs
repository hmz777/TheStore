using FluentAssertions;
using TheStore.Catalog.Core.Aggregates.Categories;

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
			var action = () => new Category(order, name, false);

			action.Should().Throw<Exception>();
		}

		[Fact]
		public void Can_Create_Valid_Category()
		{
			var action = () => new Category(1, "name", false);

			action.Should().NotThrow<Exception>();
		}
	}
}
