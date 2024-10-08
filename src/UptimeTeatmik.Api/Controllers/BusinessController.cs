using MediatR;
using Microsoft.AspNetCore.Mvc;
using UptimeTeatmik.Application.Businesses.Commands.SubscribeToBusiness;
using UptimeTeatmik.Application.Businesses.Queries.GetBusiness;
using UptimeTeatmik.Application.Businesses.Queries.SearchForBusinesses;
using UptimeTeatmik.Application.Businesses.Queries.UpdatesBusinesses;
using UptimeTeatmik.Contracts.Business;

namespace UptimeTeatmik.Api.Controllers;

[Route("/v1/businesses")]
public class BusinessController(ISender mediator) : ApiController
{
    [HttpGet("{businessId:guid}")]
    public async Task<IActionResult> GetBusiness(Guid businessId)
    {
        var query = new GetBusinessQuery(businessId);
        var result = await mediator.Send(query);
        
        return result.Match(
            Ok,
            HandleErrors
        );
    }
    
    [HttpGet("updates")]
    public async Task<IActionResult> GetUpdates([FromQuery] DateTime date)
    {
        var query = new UpdateBusinessesQuery(date);
        var result = await mediator.Send(query);

        return result.Match(
            Ok,
            HandleErrors
        );
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchForBusinesses([FromQuery] string query)
    {
        var mediatorQuery = new SearchForBusinessesQuery(query);
        var result = await mediator.Send(mediatorQuery);

        return result.Match(
            Ok,
            HandleErrors
        );
    }

    [HttpPost("subscribe")]
    public async Task<IActionResult> SubscribeToBusiness([FromBody] SubscribeToBusinessRequest request)
    {
        var query = new SubscribeToBusinessCommand(
            request.BusinessCode,
            request.SubscribersEmail,
            request.EventTypes,
            request.UpdateParameters);
        var result = await mediator.Send(query);
        
        return result.Match(
            Ok,
            HandleErrors
        );
    }
}