namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	public class MultilanguageStringDto : DtoBase
	{
		public List<LocalizedStringDto> LocalizedStrings { get; set; }
	}
}