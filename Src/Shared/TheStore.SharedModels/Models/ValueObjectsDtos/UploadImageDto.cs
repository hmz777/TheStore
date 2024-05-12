using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(UploadImageDto))]
	public class UploadImageDto : DtoBase
	{
		public IBrowserFile File { get; set; }
		public MultilanguageStringDto Alt { get; set; }
		public bool IsMainImage { get; set; }
	}
}