using Application.Models.Abstracts;
using Application.Models.Response.Pacientes;
using AutoMapper;
using Domain.Service.Pacientes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.Pacientes.Queries.GetById;

/// <summary>
/// Handler for retrieving a patient by ID
/// </summary>
public class GetByIdPacienteQueryHandler : IRequestHandler<GetByIdPacienteQueryRequest, ResponseBase<GetByIdPacienteResponseItem>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IPacienteService _pacienteService;

    /// <summary>
    /// Initializes a new instance of the GetByIdPacienteQueryHandler class
    /// </summary>
    /// <param name="mapper">AutoMapper instance for object mapping</param>
    /// <param name="logger">Logger instance for logging operations</param>
    /// <param name="pacienteService">Service for patient operations</param>
    public GetByIdPacienteQueryHandler(IMapper mapper, ILogger<GetByIdPacienteQueryHandler> logger, IPacienteService pacienteService)
    {
        _mapper = mapper;
        _logger = logger;
        _pacienteService = pacienteService;
    }

    /// <summary>
    /// Handles the retrieval of a patient by ID
    /// </summary>
    /// <param name="request">The request containing the patient ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A response containing the patient information</returns>
    /// <remarks>
    /// This handler retrieves a specific patient from the system using their unique identifier.
    /// If the patient is not found, a 404 response will be returned.
    /// </remarks>
    public async Task<ResponseBase<GetByIdPacienteResponseItem>> Handle(GetByIdPacienteQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Starting to fetch paciente with ID: {Id}", request.Id);

            var paciente = await _pacienteService.GetByIdAsync(request.Id);

            _logger.LogInformation("Successfully retrieved paciente with ID: {Id}", request.Id);

            var responseItem = _mapper.Map<GetByIdPacienteResponseItem>(paciente);

            return new ResponseBase<GetByIdPacienteResponseItem>(responseItem)
            {
                IsSuccessful = true,
                Message = $"Successfully retrieved paciente with ID: {request.Id}",
                StatusCode = StatusCodes.Status200OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching paciente with ID: {Id}", request.Id);
            return new ResponseBase<GetByIdPacienteResponseItem>(null)
            {
                IsSuccessful = false,
                Message = "An error occurred while processing your request. Please try again later.",
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
