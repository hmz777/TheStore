using FluentValidation;
using TheStore.SharedModels.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
	public class ListCartsValidator : AbstractValidator<ListRequest>
	{
        public ListCartsValidator()
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