using System.ComponentModel;

namespace TheStore.Blazor.Models.ValueObjectsDtos
{
	[DisplayName(nameof(ImageDto))]
	public class ImageDto : DtoBase
	{
		public string StringFileUri { get; set; }
		public string Alt { get; set; }
		public bool IsMainImage { get; set; }
	}
}