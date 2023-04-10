using AutoFixture;
using FluentAssertions;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Domain.Tests.AutoData.Customizations;

namespace TheStore.Domain.Tests
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

			var action = () => new Branch(name, description, fixture.Create<Address>(), fixture.Create<Image>());

			action.Should().Throw<Exception>();
		}

		[Fact]
		public void Can_Create_Valid_Branch()
		{
			var fixture = new Fixture();
			fixture.Customize(new DomainCustomization());

			var action = () => new Branch("name", "desc", fixture.Create<Address>(), fixture.Create<Image>());

			action.Should().NotThrow<Exception>();
		}
	}
}
