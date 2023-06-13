using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TheStore.ApiCommon.Data.ValueConverters
{
	public class GuidValueConverter : ValueConverter<Guid, string>
	{
		public GuidValueConverter() : base(guid => guid.ToString(), guidStr => Guid.Parse(guidStr)) { }
	}
}