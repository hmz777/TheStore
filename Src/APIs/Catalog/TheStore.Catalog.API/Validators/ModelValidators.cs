namespace TheStore.Catalog.API.Validators
{
	public static class ModelValidators
	{
		public static AddressDtoValidator AddressDtoValidator { get; } = new();
		public static CurrencyDtoValidator CurrencyDtoValidator { get; } = new();
		public static UploadImageDtoValidator UploadImageDtoValidator { get; } = new();
		public static MoneyDtoValidator MoneyDtoValidator { get; } = new();
		public static InventoryRecordDtoValidator InventoryRecordDtoValidator { get; } = new();
		public static MultilanguageStringDtoValidator MultilanguageStringDtoValidator { get; } = new();
		public static ProductColorDtoUpdateValidator ProductColorDtoUpdateValidator { get; } = new();
		public static ProductVariantDtoValidator ProductVariantDtoValidator { get; } = new();
		public static FormFileValidator FormFileValidator { get; } = new();
		public static CoordinateDtoValidator CoordinateDtoValidator { get; } = new();
		public static CultureCodeDtoValidator CultureCodeDtoValidator { get; } = new();
		public static LocalizedStringDtoValidator LocalizedStringDtoValidator { get; } = new();
	}
}
