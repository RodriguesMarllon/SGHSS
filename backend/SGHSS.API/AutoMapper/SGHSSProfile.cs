using AutoMapper;
using Application.DTOs.Pacientes;
using Api.Models.Pacientes;
using Domain.Entities.Pacientes;
using Application.Models.Response.Pacientes;
using Application.Handlers.Pacientes.Queries.GetById;

namespace Api.AutoMapper;

public class SGHSSProfile : Profile
{
    public SGHSSProfile()
    {
        CreateMapPaciente();
    }

    private void CreateMapPaciente()
    {
        CreateMap<CreatePacienteBodyModel, CreatePacienteDTO>();
        CreateMap<CreatePacienteDTO, Paciente>();
        CreateMap<Paciente, CreatePacienteResponseItem>();
        CreateMap<Paciente, GetAllPacienteResponseItem>();
        CreateMap<GetByIdPacienteQueryModel, GetByIdPacienteQueryRequest>();
        CreateMap<Paciente, GetByIdPacienteResponseItem>();
    }
}
