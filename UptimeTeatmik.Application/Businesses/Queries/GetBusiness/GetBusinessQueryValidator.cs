using FluentValidation;

namespace UptimeTeatmik.Application.Businesses.Queries.GetBusiness;

public class GetBusinessQueryValidator : AbstractValidator<GetBusinessQuery>
{
    public GetBusinessQueryValidator()
    {
        RuleFor(x => x.BusinessId)
            .NotEmpty()
            .WithMessage("BusinessId is required")
            .NotEqual(Guid.Empty)
            .WithMessage("BusinessId can't be an empty Guid");
    } 
}