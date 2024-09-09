using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Application.Businesses.Common;
using UptimeTeatmik.Application.Common.Interfaces;

namespace UptimeTeatmik.Application.Businesses.Queries.SearchForBusinesses;

public class SearchForBusinessQueryHandler(IAppDbContext dbContext) : IRequestHandler<SearchForBusinessesQuery, ErrorOr<List<BusinessResult>>>
{
    public async Task<ErrorOr<List<BusinessResult>>> Handle(SearchForBusinessesQuery request, CancellationToken cancellationToken)
    {
        var matchingBusinesses = await dbContext.Entities
            .Where(e => e.BusinessOrLastName != null && e.BusinessOrLastName.Contains(request.Query))
            .Select(e => new BusinessResult(e))
            .ToListAsync(cancellationToken: cancellationToken);

        return matchingBusinesses;
    }
}