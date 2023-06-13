using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.Infrastructure.Data.ValueConverters
{
	public class CategoryIdValueConverter : ValueConverter<CategoryId, int>
	{
		public CategoryIdValueConverter() : base(x => x.Id, x => new CategoryId(x)) { }
	}
}