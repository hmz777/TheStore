namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	public class ProductColorDto : DtoBase
	{
		public string ColorCode { get; set; }
        public List<ImageDto> Images { get; set; }
    }
}