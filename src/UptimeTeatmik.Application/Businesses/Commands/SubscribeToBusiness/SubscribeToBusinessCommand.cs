using ErrorOr;
using MediatR;

namespace UptimeTeatmik.Application.Businesses.Commands.SubscribeToBusiness;

public record SubscribeToBusinessCommand(string BusinessCode, string SubscribersEmail) : IRequest<ErrorOr<Success>>;