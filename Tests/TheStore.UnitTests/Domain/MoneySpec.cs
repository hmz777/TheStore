using FluentAssertions;
using TheStore.Catalog.Core.Exceptions;
using TheStore.Catalog.Core.ValueObjects;

namespace TheStore.Tests.Domain
{
    public class MoneySpec
    {
        [Theory]
        [InlineData(-5000, "USD")]
        [InlineData(5000, null)]
        public void Cant_Create_Invalid_Money(int amount, string currency)
        {
            var action = () => new Money(amount, new Currency(currency));

            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(5000, "EUR", 5000, "USD")]
        [InlineData(10000, "EUR", 1000, "EUR")]
        [InlineData(4500, "USD", 2500, "EUR")]
        public void Money_Should_Not_Be_Equal(int amount1, string currency1, int amount2, string currency2)
        {
            var money1 = new Money(amount1, new Currency(currency1));
            var money2 = new Money(amount2, new Currency(currency2));

            money1.Should().NotBe(money2);
        }

        [Fact]
        public void Can_Add_Two_Money()
        {
            var money1 = new Money(5000, new Currency("USD"));
            var money2 = new Money(10000, new Currency("USD"));

            var money3 = money1 + money2;

            money3.Amount.Should().Be(15000);
        }

        [Fact]
        public void Can_Subtract_Two_Money()
        {
            var money1 = new Money(10000, new Currency("USD"));
            var money2 = new Money(5000, new Currency("USD"));

            var money3 = money1 - money2;

            money3.Amount.Should().Be(5000);
        }

        [Fact]
        public void Cant_Subtract_To_Zero_Amount()
        {
            var money1 = new Money(2000, new Currency("USD"));
            var money2 = new Money(5000, new Currency("USD"));

            var action = () => money1 - money2;

            action.Should().Throw<MoneyAmountNotPositiveException>();
        }

        [Fact]
        public void Cant_Add_Money_With_Different_Currencies()
        {
            var money1 = new Money(5000, new Currency("USD"));
            var money2 = new Money(10000, new Currency("EUR"));

            var action = () => money1 + money2;

            action.Should().Throw<MoneyNotCompatibleException>();
        }

        [Fact]
        public void Cant_Subtract_Money_With_Different_Currencies()
        {
            var money1 = new Money(50000, new Currency("USD"));
            var money2 = new Money(10000, new Currency("EUR"));

            var action = () => money1 - money2;

            action.Should().Throw<MoneyNotCompatibleException>();
        }
    }
}