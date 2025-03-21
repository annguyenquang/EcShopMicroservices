using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviours;

public class LoggingBehaviour<TRequest, TResponse>
    (ILogger<LoggingBehaviour<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation($"[START] Handle request={typeof(TRequest).Name} - Response={typeof(TResponse).Name}, Request Data={request}");
          
        var timer = new Stopwatch();

        timer.Start();

        var result = await next();

        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning($"[PERFORMANCE] The request {typeof(TRequest).Name} taken {timeTaken}");
        }

        logger.LogInformation($"[END] Handled {typeof(TRequest).Name} with {result}");
        
        
        return result;
    }
}
