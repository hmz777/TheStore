using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(CoordinateDto))]
	public class CoordinateDto : DtoBase
	{
		public float Latitude { get; set; }
		public float Longitude { get; set; }
	}
}