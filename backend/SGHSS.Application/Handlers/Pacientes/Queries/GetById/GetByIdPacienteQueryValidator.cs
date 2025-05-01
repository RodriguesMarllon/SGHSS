using FluentValidation;

namespace Application.Handlers.Pacientes.Queries.GetById;

public class GetByIdPacienteQueryValidator : AbstractValidator<GetByIdPacienteQueryRequest>
{
    public GetByIdPacienteQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Paciente ID is required")
            .NotEqual(Guid.Empty)
            .WithMessage("Paciente ID cannot be empty");
    }
}
