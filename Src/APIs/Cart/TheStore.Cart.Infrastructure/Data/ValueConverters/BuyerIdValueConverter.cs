using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheStore.Cart.Core.ValueObjects.Keys;

namespace TheStore.Cart.Infrastructure.Data.ValueConverters
{
	public class BuyerIdValueConverter : ValueConverter<BuyerId, string>
	{
		public BuyerIdValueConverter() : base(buyerId => buyerId.ToString(),
			stringBuyerId => new BuyerId(Guid.Parse(stringBuyerId)))
		{ }
	}
}