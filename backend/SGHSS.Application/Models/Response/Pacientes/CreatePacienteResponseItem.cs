namespace Application.Models.Response.Pacientes;

/// <summary>
/// Response model for created patient information
/// </summary>
public class CreatePacienteResponseItem
{
    /// <summary>
    /// Full name of the created patient
    /// </summary>
    /// <example>John Doe</example>
    public string Name { get; set; }

    /// <summary>
    /// CPF of the created patient
    /// </summary>
    /// <example>12345678900</example>
    public string CPF { get; set; }

    /// <summary>
    /// Birth date of the created patient
    /// </summary>
    /// <example>1990-01-01</example>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Phone number of the created patient
    /// </summary>
    /// <example>(11) 99999-9999</example>
    public string Phone { get; set; }

    /// <summary>
    /// Email address of the created patient
    /// </summary>
    /// <example>john.doe@example.com</example>
    public string Email { get; set; }

    /// <summary>
    /// Full address of the created patient
    /// </summary>
    /// <example>123 Main St, City, State, 12345</example>
    public string Address { get; set; }
}
