using FluentValidation;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class RemovePartValidator : AbstractValidator<RemovePartRequest>
	{
		public RemovePartValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.PartId)
				.NotEmpty();
		}
	}
}
