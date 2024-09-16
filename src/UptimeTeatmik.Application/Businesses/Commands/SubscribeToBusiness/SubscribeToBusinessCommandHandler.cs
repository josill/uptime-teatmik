using ErrorOr;
using MediatR;
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

        var subscription = new Subscription()
        {
            SubscribedBusinessId = business.Id,
            SubscribersEmail = command.SubscribersEmail,
            EventTypes = command.EventTypes ?? [],
            UpdateParameters = command.UpdateParameters ?? [],
        };
        
        // TODO: Check for existing subscription or create one 
        return new Success();
    }
}