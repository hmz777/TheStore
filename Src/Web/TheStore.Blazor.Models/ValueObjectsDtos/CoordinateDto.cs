using System.ComponentModel;
using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.ValueObjectsDtos
{
	[DisplayName(nameof(CoordinateDto))]
	public class CoordinateDto : DtoBase
	{
		public float Latitude { get; set; }
		public float Longitude { get; set; }
	}
}