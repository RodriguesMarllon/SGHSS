using System.Net;

namespace Domain.Interfaces.Requests
{
    public class ResponseExternalApi<T>
    {
        public bool IsSuccessful { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public ResponseExternalApi()
        {
        }

        public ResponseExternalApi(bool isSuccessful, int statusCode, string? message, T? data = default)
        {
            IsSuccessful = isSuccessful;
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public ResponseExternalApi(T? data)
        {
            IsSuccessful = true;
            StatusCode = (int)HttpStatusCode.OK;
            Message = HttpStatusCode.OK.ToString();
            Data = data;
        }

        public static ResponseExternalApi<T> Success(T? data)
        {
            return new ResponseExternalApi<T>(true, (int)HttpStatusCode.OK, "Request was successful", data);
        }

        public static ResponseExternalApi<T> Error(int statusCode, string message)
        {
            return new ResponseExternalApi<T>(false, statusCode, message);
        }
    }
}
