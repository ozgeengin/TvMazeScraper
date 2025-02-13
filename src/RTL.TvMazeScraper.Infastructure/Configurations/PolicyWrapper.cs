using Microsoft.Extensions.Logging;
using Polly.RateLimit;
using Polly;
using System.Net;
using RTL.TvMazeScraper.Infastructure.Models.Settings;

namespace RTL.TvMazeScraper.Infastructure.Configurations
{
    public static class PolicyWrapper
    {
        public static IAsyncPolicy CreateRateLimiterPolicy(ILogger logger, RateLimiterSettings rateLimiterSettings)
        {
            var waitAndRetry = Policy
                .Handle<RateLimitRejectedException>()
                .WaitAndRetryForeverAsync(attempt => TimeSpan.FromSeconds(attempt), (_, timeSpan) =>
                {
                    logger.LogInformation($"RateLimiter. Delay = {timeSpan.TotalMilliseconds} ms.");
                });

            var rateLimiterPolicy = Policy.RateLimitAsync(
                rateLimiterSettings.Threshold,
                TimeSpan.FromSeconds(rateLimiterSettings.TimeFrameInSeconds),
                rateLimiterSettings.MaxBurst
                );

            return waitAndRetry.WrapAsync(rateLimiterPolicy);
        }

        public static IAsyncPolicy<HttpResponseMessage> CreateHttpClientPolicy(ILogger logger, int retryCount)
        {
            return Policy
                .HandleResult<HttpResponseMessage>(responseMessage =>
                    responseMessage.StatusCode is HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(retryCount,
                    attempt => TimeSpan.FromSeconds(attempt),
                    (exception, calculatedWaitDuration) =>
                    {
                        logger.LogError(
                            $"TooManyRequests. Delay = {calculatedWaitDuration.TotalMilliseconds} ms. Error status code = {exception.Result.StatusCode}");
                    }
                );
        }
    }
}
