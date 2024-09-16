using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;
using UptimeTeatmik.Domain.Errors;
using UptimeTeatmik.Domain.Models;

namespace UptimeTeatmik.Application.Businesses.Commands.SubscribeToBusiness;

public class SubscribeToBusinessCommandHandler(IAppDbContext dbContext, IBusinessRegisterService businessRegisterService) : IRequestHandler<SubscribeToBusinessCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(SubscribeToBusinessCommand command, CancellationToken cancellationToken)
    {
        var business = await businessRegisterService.UpdateBusinessAsync(command.BusinessCode);
        if (business == null) return Errors.Business.FailureGettingBusiness(command.BusinessCode);
        
        var existingSubscription = await dbContext.Subscriptions
            .FirstOrDefaultAsync(s => s.SubscribedBusinessId == business.Id, cancellationToken: cancellationToken);
        if (existingSubscription == null)
        {
            var subscription = new Subscription()
            {
                SubscribedBusinessId = business.Id,
                SubscribersEmail = command.SubscribersEmail,
                EventTypes = command.EventTypes ?? [],
                UpdateParameters = command.UpdateParameters ?? [],
            };
            dbContext.Subscriptions.Add(subscription);
        }
        else
        {
            if (command.EventTypes != null)
            {
                existingSubscription.EventTypes = existingSubscription.EventTypes
                    .Union(command.EventTypes)
                    .ToList();
            }

            if (command.UpdateParameters != null)
            {
                existingSubscription.UpdateParameters = existingSubscription.UpdateParameters
                    .Union(command.UpdateParameters)
                    .ToList();
            }
        }
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}