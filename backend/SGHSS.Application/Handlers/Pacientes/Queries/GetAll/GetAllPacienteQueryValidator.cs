using FluentValidation;

namespace Application.Handlers.Pacientes.Queries.GetAll;

public class GetAllPacienteQueryValidator : AbstractValidator<GetAllPacienteQueryRequest>
{
    public GetAllPacienteQueryValidator()
    {
        // Não há validações necessárias para buscar todos os pacientes
    }
} 