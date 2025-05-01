namespace Application.Models.Response.Pacientes;

public class CreatePacienteResponseItem
{
    public string Name { get; set; }
    public string CPF { get; set; }
    public DateTime BirthDate { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}
