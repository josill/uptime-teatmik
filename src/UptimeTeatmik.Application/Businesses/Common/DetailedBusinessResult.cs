using UptimeTeatmik.Domain.Models;

namespace UptimeTeatmik.Application.Businesses.Common;

public record DetailedBusinessResult(Entity Entity, List<Entity> Owners);
