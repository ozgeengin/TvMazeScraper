using Microsoft.Extensions.Logging;
using Polly.RateLimit;
using Polly;
using System.Net;
using RTL.TvMazeScraper.Infastructure.Settings;

namespace RTL.TvMazeScraper.Infastructure.Configurations
{
    // todo check again
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

        public static IAsyncPolicy<HttpResponseMessage> CreateHttpClientPolicy(ILogger logger, CircuitBreakerSettings circuitBreakerSettings)
        {
            var waitAndRetryPolicy = Policy
                .HandleResult<HttpResponseMessage>(responseMessage =>
                    responseMessage.StatusCode is
                        HttpStatusCode.ServiceUnavailable or
                        HttpStatusCode.Unauthorized or
                        HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(circuitBreakerSettings.RetryTime,
                    attempt => TimeSpan.FromSeconds(attempt),
                    (exception, calculatedWaitDuration) =>
                    {
                        logger.LogError(
                            $"Retry. Delay = {calculatedWaitDuration.TotalMilliseconds} ms. Error status code = {exception.Result.StatusCode}");
                    }
                );

            var circuitBreakerPolicyForRecoverable = Policy
                .HandleResult<HttpResponseMessage>(responseMessage =>
                    responseMessage.StatusCode is
                        HttpStatusCode.InternalServerError or
                        HttpStatusCode.BadGateway or
                        HttpStatusCode.GatewayTimeout
                )
                .CircuitBreakerAsync(
                    circuitBreakerSettings.HandledEventsAllowedBeforeBreaking,
                    TimeSpan.FromSeconds(circuitBreakerSettings.CircuitBreakerDurationOnBreakInSeconds),
                    (outcome, breakDelay) =>
                    {
                        logger.LogInformation(
                            $"Circuit breaker. Delay = {breakDelay.TotalMilliseconds} ms. Error status code = {outcome.Result.StatusCode}");
                    },
                    () => logger.LogInformation("Circuit breaker reset.")
                );

            return Policy.WrapAsync(waitAndRetryPolicy, circuitBreakerPolicyForRecoverable);
        }
    }
}
