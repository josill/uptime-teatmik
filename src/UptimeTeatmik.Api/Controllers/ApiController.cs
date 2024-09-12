using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UptimeTeatmik.Api.Controllers;

[ApiController]
// [Authorize]
public class ApiController : ControllerBase
{
    protected IActionResult HandleErrors(List<Error> errors)
    {
        if (errors.Count == 0) return Problem();
        
        if (errors.TrueForAll(e => e.Type == ErrorType.Validation))
        {
            return CreateValidationProblem(errors);
        }
        
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;
        
        return Problem(errors[0]);
    }

    private ObjectResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
        
        return Problem(statusCode: statusCode, title: error.Description);
    }

    private ActionResult CreateValidationProblem(IEnumerable<Error> errors)
    {
        var modelStateDict = new ModelStateDictionary();

        foreach (var e in errors)
        {
            modelStateDict.AddModelError(
                e.Code,
                e.Description
            );
        }
        
        return ValidationProblem(modelStateDict);
    }
}