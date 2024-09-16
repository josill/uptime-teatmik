using FluentValidation;

namespace UptimeTeatmik.Application.Businesses.Commands.SubscribeToBusiness;

public class SubscribeToBusinessCommandValidator : AbstractValidator<SubscribeToBusinessCommand>
{
    public SubscribeToBusinessCommandValidator()
    {
        RuleFor(x => x.BusinessCode)
            .NotEmpty()
            .WithMessage("BusinessCode is required");
        
        RuleFor(x => x.SubscribersEmail)
            .NotEmpty()
            .WithMessage("SubscribersEmail is required")
            .EmailAddress()
            .WithMessage("SubscribersEmail is not a valid email address");
        
        RuleFor(x => x.UpdateParameters)
            .NotEmpty()
            .WithMessage("UpdateParameters can't be an empty list")
            .When(x => x.UpdateParameters != null);
        
        RuleFor(x => x.EventTypes)
            .NotEmpty()
            .WithMessage("EventTypes can't be an empty list")
            .When(x => x.EventTypes != null);

        RuleFor(x => new { x.UpdateParameters, x.EventTypes })
            .Must(x => x.UpdateParameters != null || x.EventTypes != null)
            .WithMessage("At least one of UpdateParameters or EventTypes must be provided");
    }
}