using FluentValidation;
using TheStore.Requests.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
	public class SaveCartValidator : AbstractValidator<SaveCartRequest>
	{
		public SaveCartValidator()
		{
			RuleFor(r => r.CartId)
				.NotEmpty();
		}
	}
}
