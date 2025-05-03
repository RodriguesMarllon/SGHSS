using Application.Models.Abstracts;
using Application.Models.Response.Pacientes;
using MediatR;

namespace Application.Handlers.Pacientes.Queries.GetByCPF;

/// <summary>
/// Request model for searching a patient by CPF
/// </summary>
public class GetByCPFPacienteQueryRequest : IRequest<ResponseBase<GetByCPFPacienteResponseItem>>
{
    /// <summary>
    /// Patient's CPF (Brazilian tax ID)
    /// </summary>
    /// <example>12345678900</example>
    public string CPF { get; set; }

    /// <summary>
    /// Formatted CPF with all special characters removed
    /// </summary>
    /// <remarks>
    /// This property automatically formats the CPF by:
    /// - Removing all whitespace
    /// - Removing all dots (.)
    /// - Removing all hyphens (-)
    /// </remarks>
    public string FormattedCPF => CPF?.Trim().Replace(".", "").Replace("-", "");
}
