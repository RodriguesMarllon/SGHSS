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

/// <summary>
/// Handler for creating a new patient
/// </summary>
public class CreatePacienteBodyHandler : IRequestHandler<CreatePacienteBodyRequest, ResponseBase<CreatePacienteResponseItem>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IPacienteService _pacienteService;

    /// <summary>
    /// Initializes a new instance of the CreatePacienteBodyHandler class
    /// </summary>
    /// <param name="mapper">AutoMapper instance for object mapping</param>
    /// <param name="logger">Logger instance for logging operations</param>
    /// <param name="pacienteService">Service for patient operations</param>
    public CreatePacienteBodyHandler(IMapper mapper, ILogger<CreatePacienteBodyHandler> logger, IPacienteService pacienteService)
    {
        _mapper = mapper;
        _logger = logger;
        _pacienteService = pacienteService;
    }

    /// <summary>
    /// Handles the creation of a new patient
    /// </summary>
    /// <param name="request">The request containing patient data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A response containing the created patient information</returns>
    /// <exception cref="ApiException">Thrown when an unexpected error occurs during patient creation</exception>
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
