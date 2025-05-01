using Domain.Entities.Pacientes;
using Domain.Interfaces.Repositories.Pacientes;
using Infrastructure.Configuration;
using Infrastructure.Repositories.Abstract;

namespace Infrastructure.Repositories.Pacientes
{
    public class PacienteRepository : RepositoryBase<Paciente>, IPacienteRepository
    {
        public PacienteRepository(SGHSSContext context) : base(context)
        {
        }
    }
}
