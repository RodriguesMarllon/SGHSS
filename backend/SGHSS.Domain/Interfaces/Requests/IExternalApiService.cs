namespace Domain.Interfaces.Requests
{
    public interface IExternalApiService
    {
        Task<ResponseExternalApi<T>> GetAsync<T>(string url, Dictionary<string, string>? queryParams = null);
        Task<ResponseExternalApi<T>> PostAsync<T>(string url, object data, bool fromForm = false, Dictionary<string, string>? queryParams = null);
        Task<ResponseExternalApi<T>> PutAsync<T>(string url, object data, Dictionary<string, string>? queryParams = null);
        Task<ResponseExternalApi<T>> DeleteAsync<T>(string url, Dictionary<string, string>? queryParams = null);
    }
}
