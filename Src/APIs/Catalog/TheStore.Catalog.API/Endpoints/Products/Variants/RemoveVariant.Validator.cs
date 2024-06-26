﻿using FluentValidation;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products.Variants
{
	public class RemoveVariantValidator : AbstractValidator<RemoveVariantRequest>
	{
		public RemoveVariantValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.Sku)
				.NotEmpty();
		}
	}
}
