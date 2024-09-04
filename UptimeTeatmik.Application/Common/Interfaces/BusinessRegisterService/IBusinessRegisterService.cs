namespace UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;

public interface IBusinessRegisterService
{
    public Task<List<string>> FetchUpdatedBusinessCodesAsync(DateTime date);
    public Task UpdateBusinessesAsync(List<string> businessCodes);
}