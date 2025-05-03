using System.ComponentModel.DataAnnotations;

namespace Api.Models.Pacientes;

/// <summary>
/// Model for searching a patient by ID
/// </summary>
public class GetByIdPacienteQueryModel
{
    /// <summary>
    /// Unique identifier of the patient
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    [Required(ErrorMessage = "ID is required")]
    public Guid Id { get; set; }
}
