using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(MultilanguageStringDto))]
	public class MultilanguageStringDto : DtoBase
	{
		public List<LocalizedStringDto> LocalizedStrings { get; set; } = [];

		public override string ToString()
		{
			var culture = Thread.CurrentThread.CurrentCulture.Name;

			return LocalizedStrings.Find(ls => ls.CultureCode.Code == culture)?.Value ?? string.Empty;
		}
	}
}