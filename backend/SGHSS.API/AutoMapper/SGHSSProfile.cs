using AutoMapper;
using Application.DTOs.Pacientes;
using Application.Models.Response;
using Api.Models.Pacientes;
using Domain.Entities.Pacientes;

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
    }
}
