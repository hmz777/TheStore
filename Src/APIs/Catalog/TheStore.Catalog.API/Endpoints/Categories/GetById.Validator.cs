﻿using FluentValidation;
using TheStore.SharedModels.Models.Categories;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class GetByIdValidator : AbstractValidator<GetByIdRequest>
	{
		public GetByIdValidator()
		{
			RuleFor(x => x.CategoryId)
				.NotEmpty();
		}
	}
}
