using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LoonyBin.API.Middlewares
{
    public class FluentValidationFilter(IServiceProvider services) : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments)
            {
                if (argument.Value is null)
                    continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.Value.GetType());
                var validator = services.GetService(validatorType) as IValidator;

                if (validator is null)
                    continue;

                var result = await validator.ValidateAsync(new ValidationContext<object>(argument.Value));

                if (!result.IsValid)
                {
                    var errors = result.ToDictionary();

                    var problem = new ValidationProblemDetails(errors)
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "One or more validation errors occurred."
                    };

                    context.Result = new BadRequestObjectResult(problem);
                    return;
                }
            }

            await next();
        }
    }

}
