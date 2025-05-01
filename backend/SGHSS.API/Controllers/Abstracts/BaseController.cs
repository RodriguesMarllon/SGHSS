using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Api.Controllers.Abstracts;

[ExcludeFromCodeCoverage]
[ApiController]
public class BaseController : ControllerBase
{
    public readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public T DeserializeIfNotNull<T>(object data) where T : class
    {
        if ((data?.ToString() ?? string.Empty) != string.Empty)
        {
            var deserializedObject = JsonConvert.DeserializeObject<T>(data?.ToString() ?? string.Empty);

            if (deserializedObject != null)
            {
                return deserializedObject;
            }
        }

        throw new ArgumentNullException("O valor informado no parâmetro de entrada do endpoint não pode ser vazio.");
    }
}
