using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.Core.ValueConverters
{
	public class AssembledProductIdValueConverter : ValueConverter<AssembledProductId, int>
	{
		public AssembledProductIdValueConverter() : base(x => x.Id, x => new AssembledProductId(x)) { }
	}
}