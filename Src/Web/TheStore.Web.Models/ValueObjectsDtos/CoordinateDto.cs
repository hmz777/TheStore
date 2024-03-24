using System.ComponentModel;
using TheStore.Web.Models;

namespace TheStore.Web.Models.ValueObjectsDtos
{
	[DisplayName(nameof(CoordinateDto))]
	public class CoordinateDto : DtoBase
	{
		public float Latitude { get; set; }
		public float Longitude { get; set; }
	}
}