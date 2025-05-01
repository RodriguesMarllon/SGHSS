using System.Diagnostics.CodeAnalysis;

namespace Application.Models.Abstracts;

[ExcludeFromCodeCoverage]
public class ResponseBase<T>
{
    public bool IsSuccessful { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public ResponseBase(T? data)
    {
        Data = data;
    }
}
