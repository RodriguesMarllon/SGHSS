namespace Application.DTOs.Pacientes
{
    /// <summary>
    /// Data Transfer Object for creating a new patient
    /// </summary>
    public class CreatePacienteDTO
    {
        /// <summary>
        /// Full name of the patient
        /// </summary>
        /// <example>John Doe</example>
        public string Name { get; set; }

        /// <summary>
        /// Patient's CPF (Brazilian tax ID)
        /// </summary>
        /// <example>12345678900</example>
        public string CPF { get; set; }

        /// <summary>
        /// Patient's birth date
        /// </summary>
        /// <example>1990-01-01</example>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Patient's phone number
        /// </summary>
        /// <example>(11) 99999-9999</example>
        public string Phone { get; set; }

        /// <summary>
        /// Patient's email address
        /// </summary>
        /// <example>john.doe@example.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Patient's full address
        /// </summary>
        /// <example>123 Main St, City, State, 12345</example>
        public string Address { get; set; }
    }
}

