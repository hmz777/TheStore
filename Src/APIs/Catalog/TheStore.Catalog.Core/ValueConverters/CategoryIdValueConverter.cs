using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.Core.ValueConverters
{
	public class CategoryIdValueConverter : ValueConverter<CategoryId, int>
	{
		public CategoryIdValueConverter() : base(x => x.Id, x => new CategoryId(x)) { }
	}
}