using ErrorOr;
using MediatR;
using UptimeTeatmik.Application.Common;
using UptimeTeatmik.Application.Common.Interfaces;

namespace UptimeTeatmik.Application.Businesses.Queries.UpdatesBusinessesQuery;

public class UpdateBusinessesQueryHandler(IAppDbContext dbContext, IBusinessRegisterService businessRegisterService) : IRequestHandler<UpdateBusinessesQuery, ErrorOr<List<UpdateBusinessesResult>>>
{
    public async Task<ErrorOr<List<UpdateBusinessesResult>>> Handle(UpdateBusinessesQuery query, CancellationToken cancellationToken)
    {
        await businessRegisterService.FetchUpdates(query.Date);
        
        return new List<UpdateBusinessesResult>();
    }
}