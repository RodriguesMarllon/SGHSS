using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.Models.Abstracts;
using Microsoft.AspNetCore.Http;
using Domain.Service.Pacientes;
using Application.Models.Response.Pacientes;

namespace Application.Handlers.Pacientes.Queries.GetAll;

/// <summary>
/// Handler for retrieving all patients
/// </summary>
public class GetAllPacienteQueryHandler : IRequestHandler<GetAllPacienteQueryRequest, ResponseBase<IEnumerable<GetAllPacienteResponseItem>>>
{
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllPacienteQueryHandler> _logger;
    private readonly IPacienteService _pacienteService;

    /// <summary>
    /// Initializes a new instance of the GetAllPacienteQueryHandler class
    /// </summary>
    /// <param name="mapper">AutoMapper instance for object mapping</param>
    /// <param name="logger">Logger instance for logging operations</param>
    /// <param name="pacienteService">Service for patient operations</param>
    public GetAllPacienteQueryHandler(
        IMapper mapper, 
        ILogger<GetAllPacienteQueryHandler> logger, 
        IPacienteService pacienteService)
    {
        _mapper = mapper;
        _logger = logger;
        _pacienteService = pacienteService;
    }

    /// <summary>
    /// Handles the retrieval of all patients
    /// </summary>
    /// <param name="request">The request to get all patients</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A response containing the list of all patients</returns>
    /// <remarks>
    /// This handler retrieves all patients from the system and returns them in a list.
    /// The response includes basic information about each patient.
    /// </remarks>
    public async Task<ResponseBase<IEnumerable<GetAllPacienteResponseItem>>> Handle(GetAllPacienteQueryRequest request, CancellationToken cancellationToken)
    {
        try 
        {
            _logger.LogInformation("Starting to fetch all pacientes");

            var pacientes = await _pacienteService.GetAllAsync();
            var responseItems = _mapper.Map<IEnumerable<GetAllPacienteResponseItem>>(pacientes);

            _logger.LogInformation("Successfully retrieved {Count} pacientes", responseItems.Count());

            return new ResponseBase<IEnumerable<GetAllPacienteResponseItem>>(responseItems)   
            {
                IsSuccessful = true,
                Message = $"Successfully retrieved {responseItems.Count()} pacientes",
                StatusCode = StatusCodes.Status200OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching pacientes");
            return new ResponseBase<IEnumerable<GetAllPacienteResponseItem>>(null)
            {
                IsSuccessful = false,
                Message = "An error occurred while processing your request. Please try again later.",
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
} 