namespace Application.Models.Response.Pacientes;

public class GetByIdPacienteResponseItem
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string CPF { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string Phone { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; private set; }
}
