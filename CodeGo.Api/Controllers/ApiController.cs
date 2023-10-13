using System.Security.Claims;
using CodeGo.Api.Common.Http;
using CodeGo.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CodeGo.Api.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count == 0)
            return Problem();
        if (errors.All(error => error.Type == ErrorType.Validation))
            return ValidationProblem(errors);
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;
        var firstError = errors[0];
        return Problem(firstError);
    }

    private IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Failure => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unexpected => StatusCodes.Status406NotAcceptable,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        if (error.NumericType >= 12)
        {
            statusCode = error.NumericType switch
            {
                CustomErrorTypes.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };
        }

        return Problem(detail: error.Description, statusCode: statusCode);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        }
        return ValidationProblem(modelStateDictionary);
    }

    protected string? GetUserId()
    {
        var value = this.User.Claims.First(c => c.Type ==  ClaimTypes.NameIdentifier)?.Value;
        return value;
    }

    // HACK: Make personalized status code and return the correct ones
    protected IActionResult Created(object? value)
    {
        return StatusCode(201, value);
    }
}
