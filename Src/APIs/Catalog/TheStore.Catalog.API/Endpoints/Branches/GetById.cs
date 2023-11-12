using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.AutoMapper;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Data.Specifications.Branches;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Catalog.API.Endpoints.Branches
{
	public class GetById : EndpointBaseAsync
		.WithRequest<GetByIdRequest>
		.WithActionResult<BranchDtoUpdate>
	{
		private readonly IValidator<GetByIdRequest> validator;
		private readonly IReadApiRepository<CatalogDbContext, Branch> repository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<GetById>();

		public GetById(
			IValidator<GetByIdRequest> validator,
			IReadApiRepository<CatalogDbContext, Branch> repository,
			IMapper mapper)
		{
			this.validator = validator;
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet(GetByIdRequest.RouteTemplate, Name = GetByIdRequest.RouteName)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Gets a branch by id",
			Description = "Gets a branch by id",
			OperationId = "Branch.GetById",
			Tags = new[] { "Branches" })]
		public async override Task<ActionResult<BranchDtoUpdate>> HandleAsync(
			[FromRoute] GetByIdRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var branch = (await repository
				.FirstOrDefaultAsync(new GetBranchByIdReadSpec(request.BranchId), cancellationToken))
				.Map<Branch, BranchDtoUpdate>(mapper);

			if (branch == null)
				return NotFound();

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Get branch with Id: {Id}", request.BranchId);

			return branch;
		}
	}
}