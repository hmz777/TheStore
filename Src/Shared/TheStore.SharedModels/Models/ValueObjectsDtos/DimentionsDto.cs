using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(DimentionsDto))]
	public class DimentionsDto : DtoBase
	{
		public decimal Width { get; set; }
		public decimal Height { get; set; }
		public decimal Length { get; set; }
		public UnitOfMeasureDto Unit { get; set; }

		public override string ToString()
		{
			return $"{Width}x{Height}x{Length} {Unit}";
		}
	}
}