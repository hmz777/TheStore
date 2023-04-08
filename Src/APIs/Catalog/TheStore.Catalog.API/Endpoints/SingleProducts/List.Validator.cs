using FluentValidation;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.SingleProducts
{
	public class ListValidator : AbstractValidator<ListRequest>
	{
        public ListValidator()
        {
			RuleFor(x => x.Page)
				.NotEmpty();

			RuleFor(x => x.Take)
				.NotEmpty();
		}
    }
}
