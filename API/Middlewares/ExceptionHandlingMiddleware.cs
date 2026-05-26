namespace LoonyBin.API.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, 
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var problem = new
                {
                    title = "Internal Server Error",
                    status = 500,
                    detail = ex.Message,
                    traceId = context.TraceIdentifier
                };

                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }

}
