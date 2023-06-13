using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.Infrastructure.Data.ValueConverters
{
	public class ProductIdValueConverter : ValueConverter<ProductId, int>
	{
		public ProductIdValueConverter() : base(x => x.Id, x => new ProductId(x)) { }
	}
}