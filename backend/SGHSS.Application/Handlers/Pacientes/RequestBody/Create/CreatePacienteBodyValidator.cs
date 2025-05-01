using FluentValidation;

namespace Application.Handlers.Pacientes.RequestBody.Create;

public class CreatePacienteBodyValidator : AbstractValidator<CreatePacienteBodyRequest>
{
    public CreatePacienteBodyValidator()
    {
        RuleFor(x => x.Pacientes)
            .NotNull().WithMessage("Os dados do paciente são obrigatórios");

        RuleFor(x => x.Pacientes.Name)
            .NotEmpty().WithMessage("O nome é obrigatório")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres")
            .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("O nome deve conter apenas letras e espaços");

        RuleFor(x => x.Pacientes.CPF)
            .NotEmpty().WithMessage("O CPF é obrigatório")
            .Length(11).WithMessage("O CPF deve conter 11 dígitos")
            .Matches(@"^\d{11}$").WithMessage("O CPF deve conter apenas números")
            .Must(BeValidCPF).WithMessage("CPF inválido");

        RuleFor(x => x.Pacientes.BirthDate)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória")
            .Must(birthDate => birthDate.ToUniversalTime() < DateTime.UtcNow)
            .WithMessage("A data de nascimento não pode ser futura")
            .Must(BeValidAge).WithMessage("A idade deve estar entre 0 e 120 anos");

        RuleFor(x => x.Pacientes.Phone)
            .NotEmpty().WithMessage("O telefone é obrigatório")
            .Matches(@"^\(\d{2}\)\s\d{5}-\d{4}$").WithMessage("O telefone deve estar no formato (99) 99999-9999");

        RuleFor(x => x.Pacientes.Email)
            .NotEmpty().WithMessage("O email é obrigatório")
            .EmailAddress().WithMessage("Email inválido")
            .MaximumLength(100).WithMessage("O email deve ter no máximo 100 caracteres");

        RuleFor(x => x.Pacientes.Address)
            .NotEmpty().WithMessage("O endereço é obrigatório")
            .Length(5, 200).WithMessage("O endereço deve ter entre 5 e 200 caracteres");
    }

    private bool BeValidCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        // Remove caracteres não numéricos
        cpf = cpf.Trim().Replace(".", "").Replace("-", "");

        if (cpf.Length != 11) return false;

        // Verifica se todos os dígitos são iguais
        if (cpf.All(x => x == cpf[0])) return false;

        // Validação do primeiro dígito verificador
        int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int soma = 0;
        for (int i = 0; i < 9; i++)
            soma += int.Parse(cpf[i].ToString()) * multiplicadores1[i];

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        if (digito1 != int.Parse(cpf[9].ToString())) return false;

        // Validação do segundo dígito verificador
        int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(cpf[i].ToString()) * multiplicadores2[i];

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        return digito2 == int.Parse(cpf[10].ToString());
    }

    private bool BeValidAge(DateTime birthDate)
    {
        var birthDateUtc = birthDate.ToUniversalTime();
        var nowUtc = DateTime.UtcNow;
        var age = nowUtc.Year - birthDateUtc.Year;
        if (birthDateUtc > nowUtc.AddYears(-age)) age--;

        return age >= 0 && age <= 120;
    }
}
