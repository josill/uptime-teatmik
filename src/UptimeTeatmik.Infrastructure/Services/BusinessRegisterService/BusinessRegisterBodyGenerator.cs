using Microsoft.Extensions.Options;
using UptimeTeatmik.Application.Common.Interfaces.BusinessRegisterService;

namespace UptimeTeatmik.Infrastructure.Services.BusinessRegisterService;

public class BusinessRegisterBodyGenerator(IOptions<BusinessRegisterSettings> settings) : IBusinessRegisterBodyGenerator
{
    public string GenerateChangesUrlXmlBody(DateTime date)
    {
        return $@"<?xml version=""1.0"" encoding=""UTF-8""?>
        <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xro=""http://x-road.eu/xsd/xroad.xsd"" xmlns:iden=""http://x-road.eu/xsd/identifiers"" xmlns:prod=""http://arireg.x-road.eu/producer/"">
            <soapenv:Body>
                <prod:ettevotjaMuudatusedTasuline_v1>
                    <prod:keha>
                        <prod:ariregister_kasutajanimi>{settings.Value.Username}</prod:ariregister_kasutajanimi>
                        <prod:ariregister_parool>{settings.Value.Password}</prod:ariregister_parool>
                        <prod:ariregistri_kood>70000310</prod:ariregistri_kood>
                        <prod:kuupaev>{date:yyyy-MM-dd}</prod:kuupaev>
                    </prod:keha>
                </prod:ettevotjaMuudatusedTasuline_v1>
            </soapenv:Body>
        </soapenv:Envelope>";
    }

    public string GenerateDetailDataUrlXmlBody(string businessCode)
    {
        return $@"<?xml version=""1.0"" encoding=""UTF-8""?>
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xro=""http://x-road.eu/xsd/xroad.xsd"" xmlns:iden=""http://x-road.eu/xsd/identifiers"" xmlns:prod=""http://arireg.x-road.eu/producer/"">
                <soapenv:Body>
                 <prod:detailandmed_v2>
                     <prod:keha>
                         <prod:ariregister_kasutajanimi>{settings.Value.Username}</prod:ariregister_kasutajanimi>
                         <prod:ariregister_parool>{settings.Value.Password}</prod:ariregister_parool>
                         <prod:ariregistri_kood>{businessCode}</prod:ariregistri_kood> 
                         <prod:ariregister_valjundi_formaat>json</prod:ariregister_valjundi_formaat>
                         <prod:yandmed>1</prod:yandmed>
                         <prod:iandmed>1</prod:iandmed>
                         <prod:kandmed>0</prod:kandmed>
                         <prod:dandmed>0</prod:dandmed>
                         <prod:maarused>0</prod:maarused>
                     </prod:keha>
                 </prod:detailandmed_v2>
                </soapenv:Body>
            </soapenv:Envelope>";
    }
}