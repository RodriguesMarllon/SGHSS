using Polly;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Polly.Retry;
using Domain.Interfaces.Requests;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Requests
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly IHttpClientFactory _httpClientFactory;

        public ExternalApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public async Task<ResponseExternalApi<T>> GetAsync<T>(string url, Dictionary<string, string>? queryParams = null)
        {
            try
            {
                var response = await _retryPolicy.ExecuteAsync(() => SendRequestAsync<T>(HttpMethod.Get, url, queryParams: queryParams));

                return response;
            }
            catch (Exception ex)
            {
                return new ResponseExternalApi<T>(false, 500, $"Erro na requisição GET: {ex.Message}");
            }
        }

        public async Task<ResponseExternalApi<T>> PostAsync<T>(string url, object data, bool fromForm = false, Dictionary<string, string>? queryParams = null)
        {
            try
            {
                var response = await _retryPolicy.ExecuteAsync(() => SendRequestAsync<T>(HttpMethod.Post, url, data, fromForm, queryParams));

                return response;
            }
            catch (Exception ex)
            {
                return new ResponseExternalApi<T>(false, 500, $"Erro na requisição POST: {ex.Message}");
            }
        }

        public async Task<ResponseExternalApi<T>> PutAsync<T>(string url, object data, Dictionary<string, string>? queryParams = null)
        {
            try
            {
                var response = await _retryPolicy.ExecuteAsync(() => SendRequestAsync<T>(HttpMethod.Put, url, data, queryParams: queryParams));

                return response;
            }
            catch (Exception ex)
            {
                return new ResponseExternalApi<T>(false, 500, $"Erro na requisição PUT: {ex.Message}");
            }
        }

        public async Task<ResponseExternalApi<T>> DeleteAsync<T>(string url, Dictionary<string, string>? queryParams = null)
        {
            try
            {
                var response = await _retryPolicy.ExecuteAsync(() => SendRequestAsync<T>(HttpMethod.Delete, url, queryParams: queryParams));

                return response;
            }
            catch (Exception ex)
            {
                return new ResponseExternalApi<T>(false, 500, $"Erro na requisição DELETE: {ex.Message}");
            }
        }

        private async Task<ResponseExternalApi<T>> SendRequestAsync<T>(HttpMethod method, string url, object? data = null, bool fromForm = false, Dictionary<string, string>? queryParams = null)
        {
            // Adiciona query parameters na URL se existirem
            if (queryParams != null && queryParams.Any())
            {
                var queryString = string.Join("&", queryParams.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));
                url = $"{url}?{queryString}";
            }

            var request = new HttpRequestMessage(method, url);

            // Verifica se o método é POST ou PUT e se há dados para enviar
            if ((method == HttpMethod.Post || method == HttpMethod.Put) && data != null)
            {
                if (fromForm)
                {
                    var formContent = new MultipartFormDataContent();

                    // Se data contiver um arquivo (IFormFile), adiciona o arquivo ao conteúdo do formulário
                    if (data is not null)
                    {
                        var dataProperties = data.GetType().GetProperties();
                        foreach (var property in dataProperties)
                        {
                            var value = property.GetValue(data);
                            if (value is IFormFile formFile)
                            {
                                var fileContent = new StreamContent(formFile.OpenReadStream());
                                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(formFile.ContentType);
                                formContent.Add(fileContent, property.Name, formFile.FileName);
                            }
                            else
                            {
                                var stringContent = new StringContent(value.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                                formContent.Add(stringContent, property.Name);
                            }
                        }
                    }

                    request.Content = formContent;
                }
                else
                {
                    // Se 'fromForm' for false, serializa os dados como JSON
                    var json = JsonConvert.SerializeObject(data);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }
            }

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromSeconds(600);
            var response = await httpClient.SendAsync(request);

            // Processa a resposta da API
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var responseDataDeserialized = JsonConvert.DeserializeObject<ResponseExternalApi<T>>(responseData);
                return new ResponseExternalApi<T>(true, (int)response.StatusCode, null, responseDataDeserialized.Data);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return new ResponseExternalApi<T>(false, (int)response.StatusCode, $"Erro: {errorMessage}");
            }
        }

    }
}
