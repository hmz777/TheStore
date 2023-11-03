using FluentValidation;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.API.Validators
{
	public class CoordinateDtoValidator : AbstractValidator<CoordinateDto>
	{
		public CoordinateDtoValidator()
		{
			RuleFor(x => x.Latitude)
				.NotEmpty();

			RuleFor(x => x.Longitude)
				.NotEmpty();
		}
	}
}