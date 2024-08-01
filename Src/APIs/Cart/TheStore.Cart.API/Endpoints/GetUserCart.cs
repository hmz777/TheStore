using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Cart.Core.ValueObjects.Keys;
using TheStore.Cart.Infrastructure.Data;
using TheStore.Cart.Infrastructure.Data.Specifications;
using TheStore.Cart.Infrastructure.Helpers;
using TheStore.Requests;
using TheStore.Requests.Models.Cart;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
    public class GetUserCart : EndpointBaseAsync
        .WithRequest<GetUserCartRequest>
        .WithActionResult<Result<CartDto>>
    {
        private readonly IValidator<GetUserCartRequest> validator;
        private readonly IReadApiRepository<CartDbContext, Core.Aggregates.Cart> apiRepository;
        private readonly IMapper mapper;
        private readonly Serilog.ILogger log = Log.ForContext<GetUserCart>();

        public GetUserCart(
            IValidator<GetUserCartRequest> validator,
            IReadApiRepository<CartDbContext, Core.Aggregates.Cart> apiRepository,
            IMapper mapper)
        {
            this.validator = validator;
            this.apiRepository = apiRepository;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet(GetUserCartRequest.RouteTemplate, Name = GetUserCartRequest.RouteName)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Gets a cart associated with a certain user",
            Description = "Gets a cart associated with a certain user",
            OperationId = "Cart.Get",
            Tags = ["Carts"])]
        public async override Task<ActionResult<Result<CartDto>>> HandleAsync(
        [FromRoute] GetUserCartRequest request,
        CancellationToken cancellationToken = default)
        {
            var validation = await validator.ValidateAsync(request, cancellationToken);
            if (validation.IsValid == false)
                return BadRequest(Result.Failure(validation.AsErrors(), Constants.ValidationMessages.InvalidSubmittedData));

            var buyerId = new BuyerId(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)!));

            var cart = await apiRepository
                .FirstOrDefaultAsync(new GetCartByBuyerIdSpec(buyerId), cancellationToken);

            if (cart == null)
                return NotFound(Result.Failure(Constants.ValidationMessages.CartNotFound));

            var cartDto = mapper.Map<Core.Aggregates.Cart, CartDto>(cart);

            using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
                log.Information("Get cart with id: {CartId}", cart.Id);

            return Result.Success(cartDto);
        }
    }
}