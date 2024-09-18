using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Application.Businesses.Common;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Domain.Errors;

namespace UptimeTeatmik.Application.Businesses.Queries.GetBusiness;

public class GetBusinessQueryHandler(IAppDbContext dbContext) : IRequestHandler<GetBusinessQuery, ErrorOr<DetailedBusinessResult?>>
{
    public async Task<ErrorOr<DetailedBusinessResult?>> Handle(GetBusinessQuery request, CancellationToken cancellationToken)
    {
        var business = await dbContext.Entities
            .Where(e => e.Id == request.BusinessId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (business == null) return Errors.Business.BusinessNotFound(request.BusinessId);

        var owners = await dbContext.EntityOwners
            .Where(o => o.OwnedId == request.BusinessId)
            .Select(e => e.Owner)
            .ToListAsync(cancellationToken: cancellationToken);

        return new DetailedBusinessResult(business, owners);
    }
}