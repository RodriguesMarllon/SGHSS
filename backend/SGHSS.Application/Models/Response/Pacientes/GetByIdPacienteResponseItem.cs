namespace Application.Models.Response.Pacientes;

/// <summary>
/// Response model for detailed patient information
/// </summary>
public class GetByIdPacienteResponseItem
{
    /// <summary>
    /// Unique identifier of the patient
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    public Guid Id { get; private set; }

    /// <summary>
    /// Full name of the patient
    /// </summary>
    /// <example>John Doe</example>
    public string Name { get; private set; }

    /// <summary>
    /// Patient's CPF (Brazilian tax ID)
    /// </summary>
    /// <example>12345678900</example>
    public string CPF { get; private set; }

    /// <summary>
    /// Patient's birth date
    /// </summary>
    /// <example>1990-01-01</example>
    public DateTime BirthDate { get; private set; }

    /// <summary>
    /// Patient's phone number
    /// </summary>
    /// <example>(11) 99999-9999</example>
    public string Phone { get; private set; }

    /// <summary>
    /// Patient's email address
    /// </summary>
    /// <example>john.doe@example.com</example>
    public string Email { get; private set; }

    /// <summary>
    /// Patient's full address
    /// </summary>
    /// <example>123 Main St, City, State, 12345</example>
    public string Address { get; private set; }

    /// <summary>
    /// Date and time when the patient record was created
    /// </summary>
    /// <example>2024-03-20T10:00:00Z</example>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Indicates if the patient record is active
    /// </summary>
    /// <example>true</example>
    public bool IsActive { get; private set; }
}
