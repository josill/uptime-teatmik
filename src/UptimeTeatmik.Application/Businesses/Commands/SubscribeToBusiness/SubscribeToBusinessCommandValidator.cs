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
    }
}