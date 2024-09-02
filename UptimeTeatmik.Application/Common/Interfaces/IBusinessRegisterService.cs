namespace UptimeTeatmik.Application.Common.Interfaces;

public interface IBusinessRegisterService
{
    public Task<List<string>> FetchUpdates(DateTime date);
}