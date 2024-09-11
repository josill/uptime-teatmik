using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace UptimeTeatmik.Api.Controllers;

public class UptimeTeatmikProblemDetailsFactory(IOptions<ApiBehaviorOptions> options) : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options = options.Value;

    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext, 
        int? statusCode = null, 
        string? title = null,
        string? type = null, 
        string? detail = null, 
        string? instance = null
    )
    {
        statusCode ??= 500;
        
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };
        
        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);
        
        return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary, 
        int? statusCode = null, 
        string? title = null, 
        string? type = null,
        string? detail = null, 
        string? instance = null)
    {
        statusCode ??= 400;
        
        var errors = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Title = title ?? "One or more validation errors occurred.",
            Type = type,
            // Detail = detail,
            Instance = instance
        };
        
        ApplyProblemDetailsDefaults(httpContext, errors, statusCode.Value);
        
        return errors;
    }

    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;
        
        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }
        
       
        

        if (httpContext.Items[HttpContextItemKeys.Errors] is List<Error> errors)
        {
            problemDetails.Extensions[HttpContextItemKeys.Errors] = errors;
        }    
    }
}