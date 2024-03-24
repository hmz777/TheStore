using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TheStore.Web.Models.ValueObjectsDtos
{
	[DisplayName(nameof(UpdateImageDto))]
	public class UpdateImageDto : DtoBase
	{
		public string Alt { get; set; }

		[JsonIgnore]
		public string? StringFileUri { get; set; }

		public bool IsMainImage { get; set; }
	}
}