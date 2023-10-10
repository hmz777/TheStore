using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Text.Json.Serialization;
using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.ValueObjectsDtos
{
	[DisplayName(nameof(UpdateImageDto))]
	public class UpdateImageDto : DtoBase
	{
		public IFormFile File { get; set; }
		public string Alt { get; set; }

		[JsonIgnore]
		public string? StringFileUri { get; set; }

		public bool IsMainImage { get; set; }
	}
}