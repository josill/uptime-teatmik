using MediatR;
using Microsoft.AspNetCore.Mvc;
using UptimeTeatmik.Application.Businesses.Queries.UpdatesBusinessesQuery;

namespace UptimeTeatmik.Api.Controllers;

public class BusinessController(ISender mediator) : ApiController
{
    [HttpGet("updates")]
    public async Task<IActionResult> GetUpdates([FromQuery] DateTime date)
    {
        var query = new UpdateBusinessesQuery(date);
        var result = await mediator.Send(query);

        return result.Match(
            Ok,
            Problem
        );
    }
}