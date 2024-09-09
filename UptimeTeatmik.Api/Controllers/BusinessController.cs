using MediatR;
using Microsoft.AspNetCore.Mvc;
using UptimeTeatmik.Application.Businesses.Queries.UpdatesBusinesses;

namespace UptimeTeatmik.Api.Controllers;

[Route("/v3/businesses")]
public class BusinessController(ISender mediator) : ApiController
{
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

    [HttpGet]
    public async Task<IActionResult> SearchForBusinesses([FromQuery] string query)
    {
        var mediatorQuery = new SearchForBusinessesQuery(query);
        var result = await mediator.Send(mediatorQuery);

        return result.Match(
            Ok,
            HandleErrors
        );
    }
}