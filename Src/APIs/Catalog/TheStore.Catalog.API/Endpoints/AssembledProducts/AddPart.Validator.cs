﻿using FluentValidation;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class AddPartValidator : AbstractValidator<AddPartRequest>
	{
		public AddPartValidator()
		{
			RuleFor(x => x.ProductId)
				.NotEmpty();

			RuleFor(x => x.PartId)
				.NotEmpty();
		}
	}
}
