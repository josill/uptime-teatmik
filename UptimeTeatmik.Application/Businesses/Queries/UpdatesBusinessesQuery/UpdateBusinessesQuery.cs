using ErrorOr;
using MediatR;

namespace UptimeTeatmik.Application.Businesses.Queries.UpdatesBusinessesQuery;

public record UpdateBusinessesQuery(DateTime Date) : IRequest<ErrorOr<List<UpdateBusinessesResult>>>;

public record UpdateBusinessesResult
{
    public string BusinessCode { get; set; } = null!;
}