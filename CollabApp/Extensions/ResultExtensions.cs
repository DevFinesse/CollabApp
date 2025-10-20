using Microsoft.AspNetCore.Mvc;

namespace CollabApp.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToProblem(this CollabApp.Shared.Abstractions.Result result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Cannot convert successful result to problem");

            var problemDetails = new ProblemDetails
            {
                Title = result.Error.Code,
                Detail = result.Error.Description,
                Status = result.Error.statusCode ?? 400
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = result.Error.statusCode ?? 400
            };
        }

        public static IActionResult ToProblem<T>(this CollabApp.Shared.Abstractions.Result<T> result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Cannot convert successful result to problem");

            var problemDetails = new ProblemDetails
            {
                Title = result.Error.Code,
                Detail = result.Error.Description,
                Status = result.Error.statusCode ?? 400
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = result.Error.statusCode ?? 400
            };
        }
    }
}
