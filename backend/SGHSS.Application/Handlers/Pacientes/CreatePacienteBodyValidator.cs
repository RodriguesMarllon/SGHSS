using System;
using FluentValidation;
using Application.Models.Response;
using Application.Models.Abstracts;
using Application.DTOs.Pacientes;

namespace Application.Handlers.Pacientes;

public class CreatePacienteBodyValidator : AbstractValidator<CreatePacienteDTO>
{
    public CreatePacienteBodyValidator()
    {
        // Name validation
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(3, 100).WithMessage("Name must be between 3 and 100 characters")
            .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("Name must contain only letters and spaces");

        // CPF validation
        RuleFor(x => x.CPF)
            .NotEmpty().WithMessage("CPF is required")
            .Length(11).WithMessage("CPF must contain 11 digits")
            .Matches(@"^\d{11}$").WithMessage("CPF must contain only numbers")
            .Must(BeValidCPF).WithMessage("Invalid CPF");

        // Birth date validation
        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Birth date is required")
            .LessThan(DateTime.Now).WithMessage("Birth date cannot be in the future")
            .Must(BeValidAge).WithMessage("Age must be between 0 and 120 years");

        // Phone validation
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .Matches(@"^\(\d{2}\)\s\d{5}-\d{4}$").WithMessage("Phone must be in the format (99) 99999-9999");

        // Email validation
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email")
            .MaximumLength(100).WithMessage("Email must have a maximum of 100 characters");

        // Address validation
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required")
            .Length(5, 200).WithMessage("Address must be between 5 and 200 characters");
    }

    private bool BeValidCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        // Remove non-numeric characters
        cpf = cpf.Trim().Replace(".", "").Replace("-", "");

        if (cpf.Length != 11) return false;

        // Check if all digits are the same
        if (cpf.All(x => x == cpf[0])) return false;

        // First digit validation
        int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int soma = 0;
        for (int i = 0; i < 9; i++)
            soma += int.Parse(cpf[i].ToString()) * multiplicadores1[i];

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        if (digito1 != int.Parse(cpf[9].ToString())) return false;

        // Second digit validation
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
        var age = DateTime.Now.Year - birthDate.Year;
        if (birthDate > DateTime.Now.AddYears(-age)) age--;

        return age >= 0 && age <= 120;
    }
} 