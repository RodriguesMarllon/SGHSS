using System.ComponentModel.DataAnnotations;

namespace Api.Models.Pacientes
{
    public class CreatePacienteBodyModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must have a maximum of 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CPF is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF must have 11 digits")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        public DateTime BirthDate { get; set; }

        [StringLength(20, ErrorMessage = "Phone must have a maximum of 20 characters")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email")]
        [StringLength(100, ErrorMessage = "Email must have a maximum of 100 characters")]
        public string Email { get; set; }

        [StringLength(200, ErrorMessage = "Address must have a maximum of 200 characters")]
        public string Address { get; set; }
    }
}
