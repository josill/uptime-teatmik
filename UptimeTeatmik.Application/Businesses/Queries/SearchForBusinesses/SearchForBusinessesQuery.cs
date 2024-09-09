using ErrorOr;
using MediatR;
using UptimeTeatmik.Domain;

namespace UptimeTeatmik.Application.Businesses.Queries.SearchForBusinesses;

public record SearchForBusinessesQuery(string Query) : IRequest<ErrorOr<List<BusinessResult>>>;

public record BusinessResult(Entity Entity);
