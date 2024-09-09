using ErrorOr;
using MediatR;
using UptimeTeatmik.Application.Businesses.Common;

namespace UptimeTeatmik.Application.Businesses.Queries.GetBusiness;

public record GetBusinessQuery(Guid BusinessId) : IRequest<ErrorOr<BusinessResult?>>;