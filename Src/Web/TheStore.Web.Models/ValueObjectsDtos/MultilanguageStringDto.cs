using System.ComponentModel;

namespace TheStore.Web.Models.ValueObjectsDtos
{
	[DisplayName(nameof(MultilanguageStringDto))]
	public class MultilanguageStringDto : DtoBase
	{
		public List<LocalizedStringDto> LocalizedStrings { get; set; } = [];
	}
}