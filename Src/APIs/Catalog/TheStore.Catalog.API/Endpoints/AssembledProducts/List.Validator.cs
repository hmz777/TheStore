using FluentValidation;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class ListValidator : AbstractValidator<ListAssembledRequest>
	{
		public ListValidator()
		{
			RuleFor(x => x.Page)
				.NotEmpty()
				.GreaterThanOrEqualTo(1);

			RuleFor(x => x.Take)
				.NotEmpty()
				.GreaterThanOrEqualTo(1);
		}
	}
}
