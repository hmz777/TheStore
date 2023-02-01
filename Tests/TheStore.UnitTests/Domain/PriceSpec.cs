using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Catalog.Core.Exceptions;
using TheStore.Catalog.Core.ValueObjects;

namespace TheStore.Tests.Domain
{
    public class PriceSpec
    {
        [Theory]
        [InlineData(5000, "USD", 5000, "EUR")]
        [InlineData(5000, "USD", 1000, "EUR")]
        public void Cant_Create_Valid_Price(decimal moneyAmount, string moneyCurrency, decimal taxAmount, string taxCurrency)
        {
            var action = () => new Price(new Money(moneyAmount, new Currency(moneyCurrency)), new Tax("Shipping", taxAmount, new Currency(taxCurrency)));

            action.Should().Throw<MoneyNotCompatibleException>();
        }

        [Fact]
        public void Can_Create_Valid_Price()
        {
            var action = () => new Price(new Money(5000, new Currency("USD")), new Tax("Shipping", 50000, new Currency("USD")));

            action.Should().NotThrow<MoneyNotCompatibleException>();
            action.Should().NotThrow<ArgumentException>();
        }
    }
}
