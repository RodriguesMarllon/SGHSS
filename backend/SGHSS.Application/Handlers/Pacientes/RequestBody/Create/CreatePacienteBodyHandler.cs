using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.Models.Abstracts;
using Microsoft.AspNetCore.Http;
using System.Net;
using Infrastructure.CrossCutting.Utils;
using Domain.Entities.Pacientes;
using Domain.Service.Pacientes;
using Application.Models.Response.Pacientes;

namespace Application.Handlers.Pacientes.RequestBody.Create;

public class CreatePacienteBodyHandler : IRequestHandler<CreatePacienteBodyRequest, ResponseBase<CreatePacienteResponseItem>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IPacienteService _pacienteService;

    public CreatePacienteBodyHandler(IMapper mapper, ILogger<CreatePacienteBodyHandler> logger, IPacienteService pacienteService)
    {
        _mapper = mapper;
        _logger = logger;
        _pacienteService = pacienteService;
    }

    public async Task<ResponseBase<CreatePacienteResponseItem>> Handle(CreatePacienteBodyRequest request, CancellationToken cancellationToken)
    {
        try 
        {
            var paciente = await _pacienteService.CreateAsync(_mapper.Map<Paciente>(request.Pacientes));
            var responseItem = _mapper.Map<CreatePacienteResponseItem>(paciente);

            ResponseBase<CreatePacienteResponseItem> response = new(responseItem)   
            {
                IsSuccessful = true,
                Message = $"Paciente {paciente.Name} has been created successfully.",
                StatusCode = StatusCodes.Status201Created
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error on CreatePacienteBodyHandler.");
            throw new ApiException("Unexpected error on CreatePacienteBodyHandler.", HttpStatusCode.InternalServerError);
        }
    }
}
