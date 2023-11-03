using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class InventoryRecordDtoValidator : AbstractValidator<InventoryRecordDto>
	{
		public InventoryRecordDtoValidator()
		{
			RuleFor(x => x.AvailableStock)
				.GreaterThanOrEqualTo(0);

			RuleFor(x => x.RestockThreshold)
				.GreaterThan(0);

			RuleFor(x => x.MaxStockThreshold)
				.GreaterThan(0)
				.GreaterThan(x => x.RestockThreshold)
				.GreaterThanOrEqualTo(x => x.AvailableStock);

			RuleFor(x => x.OverStock)
				.GreaterThanOrEqualTo(0);
		}
	}
}
