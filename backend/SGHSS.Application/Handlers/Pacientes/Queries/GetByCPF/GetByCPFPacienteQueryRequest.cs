using Application.Models.Abstracts;
using Application.Models.Response.Pacientes;
using MediatR;

namespace Application.Handlers.Pacientes.Queries.GetByCPF;

public class GetByCPFPacienteQueryRequest : IRequest<ResponseBase<GetByCPFPacienteResponseItem>>
{
    public string CPF { get; set; }

    public string FormattedCPF => CPF?.Trim().Replace(".", "").Replace("-", "");
}
