using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(UploadImageDto))]
	public class UploadImageDto : DtoBase
	{
		public IFormFile File { get; set; }
		public MultilanguageStringDto Alt { get; set; }
		public bool IsMainImage { get; set; }
	}
}