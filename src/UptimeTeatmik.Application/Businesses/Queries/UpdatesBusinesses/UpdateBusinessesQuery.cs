using ErrorOr;
using MediatR;

namespace UptimeTeatmik.Application.Businesses.Queries.UpdatesBusinesses;

public record UpdateBusinessesQuery(DateTime Date) : IRequest<ErrorOr<UpdateBusinessesResult>>;

public record UpdateBusinessesResult
{
    public int AmountOfBusinessesUpdated { get; set; }
}