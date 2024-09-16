using ErrorOr;
using MediatR;
using UptimeTeatmik.Domain.Enums;
using UptimeTeatmik.Domain.Models;

namespace UptimeTeatmik.Application.Businesses.Commands.SubscribeToBusiness;

public record SubscribeToBusinessCommand(string BusinessCode, string SubscribersEmail, List<EventType>? EventTypes, List<string>? UpdateParameters) : IRequest<ErrorOr<SubscribeToBusinessResult>>;

public record SubscribeToBusinessResult(Subscription Subscription, int Status);