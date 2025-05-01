using Application.Models.Abstracts;
using MediatR;
using Application.DTOs.Pacientes;
using Application.Models.Response;
using FluentValidation;

namespace Application.Handlers.Pacientes.RequestBody.Create
{
    public class CreatePacienteBodyRequest : IRequest<ResponseBase<CreatePacienteResponseItem>>
    {
        public CreatePacienteDTO Pacientes { get; set; } = new CreatePacienteDTO();
    }
}

