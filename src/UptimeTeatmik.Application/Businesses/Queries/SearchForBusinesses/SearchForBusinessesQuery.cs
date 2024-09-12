using ErrorOr;
using MediatR;
using UptimeTeatmik.Application.Businesses.Common;

namespace UptimeTeatmik.Application.Businesses.Queries.SearchForBusinesses;

public record SearchForBusinessesQuery(string Query) : IRequest<ErrorOr<List<BusinessResult>>>;