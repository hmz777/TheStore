using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Catalog.Core.ValueObjects;

namespace TheStore.Tests.Domain
{
    public class TaxSpec
    {
        [Theory]
        [InlineData("", 5000, "USD")]
        [InlineData("Shipping", 0, "USD")]
        [InlineData("Shipping", -1, "USD")]
        [InlineData("Shipping", -1, null)]
        public void Cant_Create_Invalid_Tax(string taxType, decimal amount, string currency)
        {
            var action = () => new Tax(taxType, amount, new Currency(currency));

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Can_Create_Valid_Tax()
        {
            var action = () => new Tax("Shipping", 5000, new Currency("USD"));
            action.Should().NotThrow<ArgumentException>();
        }
    }
}
