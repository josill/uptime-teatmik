using FluentValidation;

namespace UptimeTeatmik.Application.Businesses.Queries.UpdatesBusinessesQuery;

public class UpdateBusinessQueryValidator : AbstractValidator<UpdateBusinessesQuery>
{
    public UpdateBusinessQueryValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Date cannot be in the future.")
            .GreaterThan(new DateTime(2000, 1, 1)).WithMessage("Date must be after January 1, 2000.");
        
    }
}