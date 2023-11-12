using AutoFixture;
using FluentAssertions;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Domain.UnitTests.AutoData.Customizations;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Domain.UnitTests
{
	public class BranchSpec
	{
		[Theory]
		[InlineData(null, "desc")]
		[InlineData("name", null)]
		public void Cant_Create_Invalid_Branch(string name, string description)
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());

			var action = () => new Branch(
				name,
				new MultilanguageString(description, CultureCode.English),
				fixture.Create<Address>(), false);

			action.Should().Throw<Exception>();
		}

		[Fact]
		public void Can_Create_Valid_Branch()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());

			var action = () => new Branch(
				fixture.Create<string>(),
				fixture.Create<MultilanguageString>(),
				fixture.Create<Address>(), false);

			action.Should().NotThrow<Exception>();
		}
	}
}
