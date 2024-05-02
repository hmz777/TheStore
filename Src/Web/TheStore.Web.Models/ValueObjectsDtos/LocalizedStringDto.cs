namespace TheStore.Web.Models.ValueObjectsDtos
{
	public class LocalizedStringDto
	{
		public CultureCodeDto CultureCode { get; set; }
		public string Value { get; set; }

		public LocalizedStringDto(CultureCodeDto cultureCode, string value)
		{
			CultureCode = cultureCode;
			Value = value;
		}
	}
}
