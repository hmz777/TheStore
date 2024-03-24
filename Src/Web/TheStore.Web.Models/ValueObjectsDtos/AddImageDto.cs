using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TheStore.Web.Models.ValueObjectsDtos
{
	[DisplayName(nameof(AddImageDto))]
	public class AddImageDto : DtoBase
	{
		public string Alt { get; set; }
		public bool IsMainImage { get; set; }

		[JsonIgnore]
		public string? StringFileUri { get; set; }
	}
}