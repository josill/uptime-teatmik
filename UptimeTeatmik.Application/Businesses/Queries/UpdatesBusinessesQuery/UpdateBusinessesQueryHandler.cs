using ErrorOr;
using MediatR;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;

namespace UptimeTeatmik.Application.Businesses.Queries.UpdatesBusinessesQuery;

public class UpdateBusinessesQueryHandler(IAppDbContext dbContext, IBusinessRegisterService businessRegisterService) : IRequestHandler<UpdateBusinessesQuery, ErrorOr<List<UpdateBusinessesResult>>>
{
    public async Task<ErrorOr<List<UpdateBusinessesResult>>> Handle(UpdateBusinessesQuery query, CancellationToken cancellationToken)
    {
        var updatedBusinesses = await businessRegisterService.FetchUpdatedBusinessCodesAsync(query.Date);
        await businessRegisterService.UpdateBusinessesAsync(updatedBusinesses);
        
        return new List<UpdateBusinessesResult>();
    }
}