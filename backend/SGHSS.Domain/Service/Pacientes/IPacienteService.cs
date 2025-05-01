using Domain.Entities.Pacientes;

namespace Domain.Service.Pacientes;

public interface IPacienteService
{
    Task<Paciente> CreateAsync(Paciente entity);
}

