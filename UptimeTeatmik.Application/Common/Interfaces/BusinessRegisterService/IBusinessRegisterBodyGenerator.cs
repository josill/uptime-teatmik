namespace UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;

public interface IBusinessRegisterBodyGenerator
{
    public string GenerateChangesUrlXmlBody(DateTime date);
    public string GenerateDetailDataUrlXmlBody(string businessCode);
}