namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	public class UnitOfMeasureDto : DtoBase
	{
		public string Unit { get; set; }

		public override string ToString()
		{
			return Unit;
		}
	}
}