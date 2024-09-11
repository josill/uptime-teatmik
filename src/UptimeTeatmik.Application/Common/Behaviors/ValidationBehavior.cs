using ErrorOr;
using FluentValidation;
using MediatR;

namespace UptimeTeatmik.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    /*
     * The validator is optional since we might not want to validate every request.
     */

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validator == null) return await next();
        
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid) return await next();

        var errors = validationResult.Errors
            .ConvertAll(validationFailure => Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage));
        
        return (dynamic) errors;
    }
}
