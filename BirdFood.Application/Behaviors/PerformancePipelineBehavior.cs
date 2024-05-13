using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BirdFood.Application.Behaviors
{
    public class PerformancePipelineBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly Stopwatch _timer;

        public PerformancePipelineBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
            _timer = new Stopwatch();
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();
             var elapsedMilliseconds = _timer.ElapsedMilliseconds;
            if(elapsedMilliseconds < 5000) {
                return response;
            }
            var requestName = typeof(TRequest).Name;
            _logger.LogWarning("Running in longtime - Request : {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                requestName, elapsedMilliseconds, request);
            return response;
        }
    }
}
