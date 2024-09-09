using ErrorOr;
using MediatR;
using UptimeTeatmik.Application.Businesses.Common;
using UptimeTeatmik.Application.Businesses.Queries.SearchForBusinesses;

namespace UptimeTeatmik.Application.Businesses.Queries.GetBusiness;

public record GetBusinessQuery(Guid BusinessId) : IRequest<ErrorOr<BusinessResult>>;