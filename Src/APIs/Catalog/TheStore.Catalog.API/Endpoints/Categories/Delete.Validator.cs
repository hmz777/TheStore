﻿using FluentValidation;
using TheStore.SharedModels.Models.Categories;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class DeleteValidator : AbstractValidator<DeleteRequest>
	{
		public DeleteValidator()
		{
			RuleFor(x => x.CategoryId)
				.NotEmpty();
		}
	}
}
