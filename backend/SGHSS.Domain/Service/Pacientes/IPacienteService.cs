using System.Linq.Expressions;
using Domain.Entities.Pacientes;

namespace Domain.Service.Pacientes;

public interface IPacienteService
{
    Task<Paciente> CreateAsync(Paciente entity);
    Task<IEnumerable<Paciente>> GetAllAsync();
    Task<Paciente> GetByIdAsync(Guid id);
    Task<IEnumerable<Paciente>> FindAllAsync(Expression<Func<Paciente, bool>> predicate);
    Task<Paciente> FindByAsync(Expression<Func<Paciente, bool>> predicate);
}

