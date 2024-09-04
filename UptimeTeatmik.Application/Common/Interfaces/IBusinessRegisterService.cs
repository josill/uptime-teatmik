using UptimeTeatmik.Domain;

namespace UptimeTeatmik.Application.Common.Interfaces;

public interface IBusinessRegisterService
{
    public Task<List<string>> FetchUpdatedBusinessCodes(DateTime date);
    public Task UpdateBusinesses(List<string> businessCodes);
}