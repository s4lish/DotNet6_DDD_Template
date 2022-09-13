using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MyApp.Filters
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exeption = context.Exception;

            var problemDetails = new ProblemDetails
            {
                Title = "An error occured Handled with Filters",
                Status = (int)HttpStatusCode.InternalServerError,

            };

            context.Result = new ObjectResult(problemDetails);

            context.ExceptionHandled = true;
        }
    }
}
