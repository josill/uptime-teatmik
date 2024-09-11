namespace UptimeTeatmik.Domain.Errors;
using ErrorOr;

public static partial class Errors
{
    public static class Business
    {
        public static Error DuplicateEmail(Guid businessId) =>
            Error.NotFound($"Business with id: {businessId} not found");
    }
}