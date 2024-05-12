namespace TheStore.Catalog.API.Validators
{
	public static class ModelValidators
	{
		public static CurrencyDtoValidator CurrencyDtoValidator => new();
		public static UploadImageDtoValidator UploadImageDtoValidator => new();
		public static MoneyDtoValidator MoneyDtoValidator => new();
		public static InventoryRecordDtoValidator InventoryRecordDtoValidator => new();
		public static MultilanguageStringDtoValidator MultilanguageStringDtoValidator => new();
		public static ProductDtoUpdateValidator ProductDtoUpdateValidator => new();
		public static ProductColorDtoUpdateValidator ProductColorDtoUpdateValidator => new();
		public static ProductVariantDtoUpdateValidator ProductVariantDtoUpdateValidator => new();
		public static FileUploadValidator FileUploadValidator => new();
		public static CoordinateDtoValidator CoordinateDtoValidator => new();
		public static CultureCodeDtoValidator CultureCodeDtoValidator => new();
		public static LocalizedStringDtoValidator LocalizedStringDtoValidator => new();
		public static AddressDtoValidator AddressDtoValidator => new();
		public static BranchDtoUpdateValidator BranchDtoUpdateValidator => new();
		public static CategoryDtoUpdateValidator CategoryDtoUpdateValidator => new();
		public static AssembledProductDtoUpdateValidator AssembledProductDtoUpdateValidator => new();
	}
}