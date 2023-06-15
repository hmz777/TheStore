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
	public class List : EndpointBaseAsync
		.WithRequest<ListRequest>
		.WithActionResult<List<BranchDto>>
	{
		private readonly IValidator<ListRequest> validator;
		private readonly IReadApiRepository<CatalogDbContext, Branch> repository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<List>();

		public List(
			IValidator<ListRequest> validator,
			IReadApiRepository<CatalogDbContext, Branch> repository,
			IMapper mapper)
		{
			this.validator = validator;
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet(ListRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Lists catalog branches",
			Description = "Lists catalog branches with pagination using skip and take",
			OperationId = "Branch.List",
			Tags = new[] { "Branches" })]
		public async override Task<ActionResult<List<BranchDto>>> HandleAsync(
			[FromQuery] ListRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var branches = (await repository
				.ListAsync(new ListBranchesPaginationDefaultOrderReadSpec(request.Take, request.Page), cancellationToken))
				.Map<Branch, BranchDto>(mapper);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("List branches with Page: {Page} and Take: {Take}", request.Page, request.Take, request.CorrelationId);

			return branches;
		}
	}
}