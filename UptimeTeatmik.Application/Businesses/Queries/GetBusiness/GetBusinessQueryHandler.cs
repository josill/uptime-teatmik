using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Application.Businesses.Common;
using UptimeTeatmik.Application.Common.Interfaces;

namespace UptimeTeatmik.Application.Businesses.Queries.GetBusiness;

public class GetBusinessQueryHandler(IAppDbContext dbContext) : IRequestHandler<GetBusinessQuery, ErrorOr<BusinessResult?>>
{
    public async Task<ErrorOr<BusinessResult?>> Handle(GetBusinessQuery request, CancellationToken cancellationToken)
    {
        var business = await dbContext.Entities
            .Where(e => e.Id == request.BusinessId)
            .Select(e => new BusinessResult(e))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return business;
    }
}