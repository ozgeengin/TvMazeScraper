using RTL.TvMazeScraper.Application.Exceptions;

namespace RTL.TvMazeScraper.WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ExceptionMiddleware> logger)
        {
            var statusCode = StatusCodes.Status500InternalServerError;

            switch (exception)
            {
                case UnauthorizedAccessException _:
                    statusCode = StatusCodes.Status401Unauthorized;
                    break;
                case ShouldBeEqualToOrBiggerThanZeroException equalToOrBiggerThanZeroException:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
                default:
                    statusCode = StatusCodes.Status404NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            logger.LogError(exception.ToString());

            return context.Response.WriteAsync(exception.Message);
        }
    }
}
