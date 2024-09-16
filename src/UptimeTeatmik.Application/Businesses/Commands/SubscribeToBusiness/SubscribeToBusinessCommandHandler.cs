using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UptimeTeatmik.Application.Common.Interfaces;
using UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;
using UptimeTeatmik.Domain.Errors;
using UptimeTeatmik.Domain.Models;

namespace UptimeTeatmik.Application.Businesses.Commands.SubscribeToBusiness;

public class SubscribeToBusinessCommandHandler(IAppDbContext dbContext, IBusinessRegisterService businessRegisterService) : IRequestHandler<SubscribeToBusinessCommand, ErrorOr<SubscribeToBusinessResult>>
{
    public async Task<ErrorOr<SubscribeToBusinessResult>> Handle(SubscribeToBusinessCommand command, CancellationToken cancellationToken)
    {
        var business = await businessRegisterService.UpdateBusinessAsync(command.BusinessCode);
        var createdNewSubscription = false;
        if (business == null) return Errors.Business.FailureGettingBusiness(command.BusinessCode);
        
        var subscription = await dbContext.Subscriptions
            .FirstOrDefaultAsync(s => s.SubscribedBusinessId == business.Id
                && s.SubscribersEmail == command.SubscribersEmail
                ,cancellationToken: cancellationToken);
        
        if (subscription == null)
        {
            var newSubscription = new Subscription()
            {
                SubscribedBusinessId = business.Id,
                SubscribersEmail = command.SubscribersEmail,
                EventTypes = command.EventTypes ?? [],
                UpdateParameters = command.UpdateParameters ?? [],
            };
            dbContext.Subscriptions.Add(newSubscription);
            createdNewSubscription = true;
            subscription = newSubscription;
        }
        else
        {
            if (command.EventTypes != null)
            {
                subscription.EventTypes = subscription.EventTypes
                    .Union(command.EventTypes)
                    .ToList();
            }

            if (command.UpdateParameters != null)
            {
                subscription.UpdateParameters = subscription.UpdateParameters
                    .Union(command.UpdateParameters)
                    .ToList();
            }
        }
        await dbContext.SaveChangesAsync(cancellationToken);

        return new SubscribeToBusinessResult(subscription, createdNewSubscription ? 201 : 204);
    }
}