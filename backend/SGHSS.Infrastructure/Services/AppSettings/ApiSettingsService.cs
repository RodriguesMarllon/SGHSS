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
                throw new ArgumentException("Nome da API informado não encontrado/configurado no AppSettings.");
            }

            if (string.IsNullOrWhiteSpace(apiConfiguration.BaseUrl))
            {
                throw new ArgumentException($"A configuração para a tag '{nameof(ApiConfiguration.BaseUrl)}' para a API '{apiName}' é inválida.");
            }

            if ((apiConfiguration.Controllers?.Count ?? 0) == 0)
            {
                throw new ArgumentException($"Não existem controllers configurados para a API '{apiName}'.");
            }

            if (apiConfiguration.Controllers.Any(controller => controller.Value.Count == 0 || controller.Value.Any(endpoints => string.IsNullOrWhiteSpace(endpoints.Value))))
            {
                throw new ArgumentException($"É necessário que ao menos um endpoint seja configurado para cada controller na API '{apiName}'.");
            }

            return apiConfiguration;
        }


    }
}
