using LoonyBin.Services.DateFilters;
using Microsoft.AspNetCore.Mvc;

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
            catch (DateFilterFormatException ex)
            {
                logger.LogWarning(ex, "Invalid date filter format");

                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors = new Dictionary<string, string[]>
                {
                    ["date"] = new[] { ex.Message }
                };

                var problem = new ValidationProblemDetails(errors)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "One or more validation errors occurred."
                };

                await context.Response.WriteAsJsonAsync(problem);
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
