﻿using FluentValidation;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Validators
{
    public class ProductColorDtoUpdateValidator : AbstractValidator<ProductColorDtoUpdate>
	{
		public ProductColorDtoUpdateValidator()
		{
			RuleFor(x => x.ColorName)
				.NotEmpty();

			RuleFor(x => x.ColorCode)
				.NotEmpty();

			RuleFor(x => x.ColorCode)
				.NotEmpty();
		}
	}
}