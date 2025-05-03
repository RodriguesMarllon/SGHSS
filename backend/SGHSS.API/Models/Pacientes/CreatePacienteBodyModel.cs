using System.ComponentModel.DataAnnotations;
using Application.Validators;

namespace Api.Models.Pacientes
{
    /// <summary>
    /// Model for creating a new patient
    /// </summary>
    public class CreatePacienteBodyModel
    {
        /// <summary>
        /// Full name of the patient
        /// </summary>
        /// <example>John Doe</example>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must have a maximum of 100 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Patient's CPF (Brazilian tax ID)
        /// </summary>
        /// <example>12345678900</example>
        [Required(ErrorMessage = "CPF is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF must have 11 digits")]
        [RegularExpression(@"^\d+$", ErrorMessage = "CPF must contain only digits")]
        [CPFValidation(ErrorMessage = "Invalid CPF number")]
        public string CPF { get; set; }

        /// <summary>
        /// Patient's birth date
        /// </summary>
        /// <example>1990-01-01</example>
        [Required(ErrorMessage = "Birth date is required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Patient's phone number
        /// </summary>
        /// <example>(11) 99999-9999</example>
        [StringLength(20, ErrorMessage = "Phone must have a maximum of 20 characters")]
        [RegularExpression(@"^\(\d{2}\)\s\d{5}-\d{4}$", ErrorMessage = "Phone must be in format (99) 99999-9999")]
        public string Phone { get; set; }

        /// <summary>
        /// Patient's email address
        /// </summary>
        /// <example>john.doe@example.com</example>
        [EmailAddress(ErrorMessage = "Invalid email")]
        [StringLength(100, ErrorMessage = "Email must have a maximum of 100 characters")]
        public string Email { get; set; }

        /// <summary>
        /// Patient's full address
        /// </summary>
        /// <example>123 Main St, City, State, 12345</example>
        [StringLength(200, ErrorMessage = "Address must have a maximum of 200 characters")]
        public string Address { get; set; }
    }
}
