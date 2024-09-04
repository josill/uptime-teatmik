using UptimeTeatmik.Domain;

namespace UptimeTeatmik.Application.Common.Interfaces;

public interface IBusinessRegisterService
{
    public Task<List<string>> FetchUpdatedBusinessCodesAsync(DateTime date);
    public Task UpdateBusinessesAsync(List<string> businessCodes);
}