using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TheStore.Blazor.Models.ValueObjectsDtos
{
	[DisplayName(nameof(AddImageDto))]
	public class AddImageDto : DtoBase
	{
		public IFormFile File { get; set; }
		public string Alt { get; set; }

		[JsonIgnore]
		public string? StringFileUri { get; set; }

		public AddImageDto()
		{

		}

		public AddImageDto(IFormFile file, string alt)
		{
			File = file;
			Alt = alt;
		}
	}
}
