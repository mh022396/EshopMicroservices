using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Shared.Behaviors;

public class LogginBehavior<TRequest, TResponse>(ILogger<LogginBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse> where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var responsetName = typeof(TResponse).Name;

        logger.LogInformation($"[START] Handle Request={requestName} - Reponse={responsetName} - RequestDate={request}");
        
        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();
        var timeTaken = timer.Elapsed;

        if (timeTaken.Seconds > 3) 
        {
            logger.LogWarning($"[PERFORMANCE] The request {requestName} took {timeTaken.Seconds}");
        }

        logger.LogInformation($"[END] Handled {requestName} with {responsetName}");
        return response;
    }
}
