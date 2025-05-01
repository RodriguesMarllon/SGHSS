using FluentValidation;

namespace Application.Handlers.Pacientes.Queries.GetByCPF;

public class GetByCPFPacienteQueryValidator : AbstractValidator<GetByCPFPacienteQueryRequest>
{
    public GetByCPFPacienteQueryValidator()
    {
        RuleFor(x => x.CPF)
            .NotEmpty().WithMessage("CPF is required.");
        
        RuleFor(x => x.FormattedCPF)
            .Length(11).WithMessage("CPF must be 11 digits long.")
            .Matches(@"^\d+$").WithMessage("CPF must contain only digits.")
            .Must(BeValidCPF).WithMessage("Invalid CPF");
    }

    private bool BeValidCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        // Remove non-numeric characters
        cpf = cpf.Trim().Replace(".", "").Replace("-", "");

        if (cpf.Length != 11) return false;

        // Check if all digits are the same
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
}
