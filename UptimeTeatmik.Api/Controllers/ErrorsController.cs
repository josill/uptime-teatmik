namespace UptimeTeatmik.Api.Controllers;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class ErrorsController: ControllerBase
{
    [HttpPost("/error")]
    [HttpGet("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        
        return Problem(exception?.Message, statusCode: 500);
    }
}