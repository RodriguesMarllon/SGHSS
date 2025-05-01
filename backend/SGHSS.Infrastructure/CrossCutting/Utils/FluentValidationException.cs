using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;

namespace Infrastructure.CrossCutting.Utils;

[ExcludeFromCodeCoverage]
public class FluentValidationException : Exception
{
    public override string Message { get; }
    public HttpStatusCode StatusCode { get; }

    public FluentValidationException(string message, HttpStatusCode statusCode=HttpStatusCode.BadRequest, Exception? innerException = null) : base(message, innerException)
    {
        Message = message;
        StatusCode = statusCode;
    }

    public override string ToString() => JsonSerializer.Serialize(this);
}
