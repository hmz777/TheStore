using FluentValidation;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
	public class ListValidator : AbstractValidator<ListRequest>
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
