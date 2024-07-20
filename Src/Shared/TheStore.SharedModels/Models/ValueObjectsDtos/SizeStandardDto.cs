namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	public class SizeStandardDto : DtoBase
	{
		public string Value { get; set; }

		public override string ToString()
		{
			return Value;
		}
	}
}
