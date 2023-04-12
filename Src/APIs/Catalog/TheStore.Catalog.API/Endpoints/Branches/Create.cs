using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Catalog.API.Endpoints.Branches
{
	public class Create : EndpointBaseAsync
		.WithRequest<CreateRequest>
		.WithActionResult<BranchDto>
	{
		private readonly IValidator<CreateRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Branch> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<Create>();

		public Create(
			IValidator<CreateRequest> validator,
			IApiRepository<CatalogDbContext, Branch> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpPost(CreateRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[SwaggerOperation(
		   Summary = "Creates a branch",
		   Description = "Creates a branch",
		   OperationId = "Branch.Create",
		   Tags = new[] { "Branches" })]
		public async override Task<ActionResult<BranchDto>> HandleAsync(
		[FromBody] CreateRequest request,
			CancellationToken cancellationToken = default)
		{
			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Create branch with name: {Name}", request.Name, request.CorrelationId);

			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var branch = await apiRepository.AddAsync(mapper.Map<Branch>(request), cancellationToken);

			return CreatedAtRoute(GetByIdRequest.RouteName, routeValues: new { BranchId = branch.Id }, mapper.Map<BranchDto>(branch));
		}
	}
}