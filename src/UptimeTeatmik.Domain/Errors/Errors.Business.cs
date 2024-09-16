namespace UptimeTeatmik.Domain.Errors;
using ErrorOr;

public static partial class Errors
{
    public static class Business
    {
        public static Error BusinessNotFound(Guid businessId) =>
            Error.NotFound($"Business with id: {businessId} not found");

        public static Error FailureGettingBusiness(string businessCode) =>
            Error.Failure(
                $"Business with code: {businessCode} was not found or encountered an error while saving entity.");
    }
}