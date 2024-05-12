using FluentValidation;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
	public class DeleteValidator : AbstractValidator<DeleteRequest>
	{
		public DeleteValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();
		}
	}
}
