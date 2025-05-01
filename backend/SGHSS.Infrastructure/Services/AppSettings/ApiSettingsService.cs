using Domain.Configuration;
using Domain.Interfaces.AppSettings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.AppSettings
{
    public class ApiSettingsService : IApiSettingsService
    {
        private readonly IOptions<ApiSettings> _apiSettings;

        public ApiSettingsService(IOptions<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings;
        }

        public ApiSettings GetApiSettings()
        {
            return _apiSettings.Value;
        }

        public ApiConfiguration GetApiConfiguration(string apiName)
        {
            ApiConfiguration foundConfiguration = null;
            var apiSettings = _apiSettings.Value;

            switch (apiName)
            {
                case nameof(apiSettings.ApiMicroExcel):
                    foundConfiguration = apiSettings.ApiMicroExcel;
                    break;
                case nameof(apiSettings.ApiMicroOpera):
                    foundConfiguration = apiSettings.ApiMicroOpera;
                    break;
            }

            return ValidateBasicConfigurationApi(foundConfiguration, apiName);
        }

        public ApiConfiguration GetApiMicroExcelConfiguration()
        {
            return GetApiConfiguration(nameof(ApiSettings.ApiMicroExcel));
        }

        public ApiConfiguration GetApiMicroOperaConfiguration()
        {
            return GetApiConfiguration(nameof(ApiSettings.ApiMicroOpera));
        }

        private ApiConfiguration ValidateBasicConfigurationApi(ApiConfiguration? apiConfiguration, string apiName)
        {
            if (apiConfiguration == null)
            {
                throw new ArgumentException("API name not found/configured in AppSettings.");
            }

            if (string.IsNullOrWhiteSpace(apiConfiguration.BaseUrl))
            {
                throw new ArgumentException($"Configuration for tag '{nameof(ApiConfiguration.BaseUrl)}' for API '{apiName}' is invalid.");
            }

            if ((apiConfiguration.Controllers?.Count ?? 0) == 0)
            {
                throw new ArgumentException($"No controllers configured for API '{apiName}'.");
            }

            if (apiConfiguration.Controllers.Any(controller => controller.Value.Count == 0 || controller.Value.Any(endpoints => string.IsNullOrWhiteSpace(endpoints.Value))))
            {
                throw new ArgumentException($"At least one endpoint must be configured for each controller in API '{apiName}'.");
            }

            return apiConfiguration;
        }


    }
}
