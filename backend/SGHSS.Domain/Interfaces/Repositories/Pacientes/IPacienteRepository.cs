using Domain.Entities.Pacientes;
using Domain.Interfaces.Repositories.Abstract;

namespace Domain.Interfaces.Repositories.Pacientes
{
    public interface IPacienteRepository : IRepositoryBase<Paciente>
    {
    }
}
