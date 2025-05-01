using Application.Models.Abstracts;
using Application.Models.Response.Pacientes;
using MediatR;

namespace Application.Handlers.Pacientes.Queries.GetById;

public class GetByIdPacienteQueryRequest : IRequest<ResponseBase<GetByIdPacienteResponseItem>>
{
    public Guid Id { get; set; }
}
