namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	public class SizeDto : DtoBase
	{
		public string Value { get; set; }
		public SizeStandardDto Standard { get; set; }
	}
}
