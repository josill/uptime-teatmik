using UptimeTeatmik.Domain;

namespace UptimeTeatmik.Application.Common.Interfaces;

public interface IBusinessRegisterService
{
    public Task<List<Business>> FetchUpdates(DateTime date);
}