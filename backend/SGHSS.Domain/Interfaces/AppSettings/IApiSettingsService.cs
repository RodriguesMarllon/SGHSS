using Domain.Configuration;

namespace Domain.Interfaces.AppSettings
{
    public interface IApiSettingsService
    {
        ApiSettings GetApiSettings();
        ApiConfiguration GetApiConfiguration(string apiName);
        ApiConfiguration GetApiMicroExcelConfiguration();
        ApiConfiguration GetApiMicroOperaConfiguration();
    }
}
