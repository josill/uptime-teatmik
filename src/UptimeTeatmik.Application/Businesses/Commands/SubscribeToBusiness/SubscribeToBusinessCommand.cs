using ErrorOr;
using MediatR;
using UptimeTeatmik.Domain.Enums;

namespace UptimeTeatmik.Application.Businesses.Commands.SubscribeToBusiness;

public record SubscribeToBusinessCommand(string BusinessCode, string SubscribersEmail, List<EventType>? EventTypes, List<string>? UpdateParameters) : IRequest<ErrorOr<Success>>;