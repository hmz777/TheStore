﻿using ApiCommon.Extensions.AutoMapper;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Catalog.Data.Specifications.Categories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.Validation;
using TheStore.Catalog.API.Data;
using TheStore.Catalog.API.Domain.Categories;
using TheStore.Catalog.API.Dtos.Category;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class GetById : EndpointBaseAsync
		.WithRequest<GetByIdRequest>
		.WithActionResult<CategoryDto>
	{
		private readonly IValidator<GetByIdRequest> validator;
		private readonly IReadRepository<Category, CatalogDbContext> repository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<GetById>();

		public GetById(
			IValidator<GetByIdRequest> validator,
			IReadRepository<Category, CatalogDbContext> repository,
			IMapper mapper)
		{
			this.validator = validator;
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet(GetByIdRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Gets a catalog category by id",
			Description = "Gets a catalog category by id",
			OperationId = "Category.GetById",
			Tags = new[] { "Categories" })]
		public async override Task<ActionResult<CategoryDto>> HandleAsync(
			[FromRoute] GetByIdRequest request,
			CancellationToken cancellationToken = default)
		{
			log.Information("Get category with Id: {CategoryId}", request.CategoryId);

			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var category = (await repository
				.FirstOrDefaultAsync(new GetCategoryByIdReadSpec(request.CategoryId), cancellationToken))
				.Map<Category, CategoryDto>(mapper);

			if (category == null)
				return NotFound();

			return category;
		}
	}
}