using MediatR;
using TheStore.SharedModels.Models;
using TheStore.Web.BlazorApp.Client.Mediator.Commands;

namespace TheStore.Web.BlazorApp.Client.Mediator.Handlers
{
    public class SaveCartHandler : IRequestHandler<SaveCartCommand, Result<decimal>>
    {
        public Task<Result<decimal>> Handle(SaveCartCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
