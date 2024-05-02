
namespace TheStore.Web.Models.ValueObjectsDtos
{
	public class CultureCodeDto
	{
		public string Code { get; set; }

		public CultureCodeDto(string code)
		{
			Code = code ?? throw new ArgumentNullException(nameof(code));
		}
	}
}