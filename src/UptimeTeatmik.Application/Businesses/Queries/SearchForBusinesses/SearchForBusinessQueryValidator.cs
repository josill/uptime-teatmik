using FluentValidation;

namespace UptimeTeatmik.Application.Businesses.Queries.SearchForBusinesses;

public class SearchForBusinessQueryValidator : AbstractValidator<SearchForBusinessesQuery>
{
    public SearchForBusinessQueryValidator()
    {
        RuleFor(x => x.Query)
            .NotEmpty()
            .WithMessage("Query is required")
            .MinimumLength(3)
            .WithMessage("Query must be at least 3 characters long");
    }
}