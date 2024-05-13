﻿using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BirdFood.Application.Behaviors
{
    public class TracingPipelineBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public TracingPipelineBehavior(ILogger<TRequest> logger)
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
            var requestName = typeof(TRequest).Name;
            _logger.LogWarning("RequestDetail : {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                requestName, elapsedMilliseconds, request);

            return response;
        }
    }
}
