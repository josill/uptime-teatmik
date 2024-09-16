using UptimeTeatmik.Domain.Models;

namespace UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;

public interface IBusinessRegisterService
{
    public Task RunBusinessUpdateJob();
    public Task<List<string>> FetchUpdatedBusinessCodesAsync(DateTime date);
    public Task<Entity?> UpdateBusinessAsync(string businessCode);
    public Task UpdateBusinessesAsync(List<string> businessCodes);
}