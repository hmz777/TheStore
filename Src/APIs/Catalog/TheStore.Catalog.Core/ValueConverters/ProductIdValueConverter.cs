using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.Core.ValueConverters
{
	public class ProductIdValueConverter : ValueConverter<ProductId, int>
	{
		public ProductIdValueConverter() : base(x => x.Id, x => new ProductId(x)) { }
	}
}