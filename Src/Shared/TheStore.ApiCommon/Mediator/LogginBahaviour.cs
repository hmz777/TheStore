using MediatR;
using Serilog;

namespace TheStore.ApiCommon.Mediator
{
	public class LogginBahaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly ILogger logger = Log.ForContext<TRequest>();

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			logger.Information($"Handling command of type: {typeof(TRequest)}, with values: {request?.ToString() ?? "No values"}");
			var response = await next();
			logger.Information($"Handled command of type: {typeof(TRequest)}, with values: {request?.ToString() ?? "No values"}");

			return response;
		}
	}
}