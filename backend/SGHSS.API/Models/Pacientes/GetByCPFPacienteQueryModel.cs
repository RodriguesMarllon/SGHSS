using System.ComponentModel.DataAnnotations;
using Application.Validators;

namespace Api.Models.Pacientes;

/// <summary>
/// Model for searching a patient by CPF
/// </summary>
public class GetByCPFPacienteQueryModel
{
    /// <summary>
    /// Patient's CPF (Brazilian tax ID)
    /// </summary>
    /// <example>12345678900</example>
    [Required(ErrorMessage = "CPF is required")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF must be 11 digits long")]
    [RegularExpression(@"^\d+$", ErrorMessage = "CPF must contain only digits")]
    [CPFValidation(ErrorMessage = "Invalid CPF number")]
    public string CPF { get; set; }
}
