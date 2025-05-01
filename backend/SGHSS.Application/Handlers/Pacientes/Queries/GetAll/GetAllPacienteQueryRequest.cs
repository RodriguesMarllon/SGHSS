using Application.Models.Abstracts;
using Application.Models.Response.Pacientes;
using MediatR;

namespace Application.Handlers.Pacientes.Queries.GetAll;

public class GetAllPacienteQueryRequest : IRequest<ResponseBase<IEnumerable<GetAllPacienteResponseItem>>>
{
}
