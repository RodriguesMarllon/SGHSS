using Application.Models.Abstracts;
using Application.Models.Response.Pacientes;
using AutoMapper;
using Domain.Service.Pacientes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.Pacientes.Queries.GetByCPF;

/// <summary>
/// Handler for retrieving a patient by CPF
/// </summary>
public class GetByCPFPacienteQueryHandler : IRequestHandler<GetByCPFPacienteQueryRequest, ResponseBase<GetByCPFPacienteResponseItem>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IPacienteService _pacienteService;

    /// <summary>
    /// Initializes a new instance of the GetByCPFPacienteQueryHandler class
    /// </summary>
    /// <param name="mapper">AutoMapper instance for object mapping</param>
    /// <param name="logger">Logger instance for logging operations</param>
    /// <param name="pacienteService">Service for patient operations</param>
    public GetByCPFPacienteQueryHandler(IMapper mapper, ILogger<GetByCPFPacienteQueryHandler> logger, IPacienteService pacienteService)
    {
        _mapper = mapper;
        _logger = logger;
        _pacienteService = pacienteService;
    }

    /// <summary>
    /// Handles the retrieval of a patient by CPF
    /// </summary>
    /// <param name="request">The request containing the patient CPF</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A response containing the patient information</returns>
    /// <remarks>
    /// This handler retrieves a specific patient from the system using their CPF.
    /// The CPF is validated and formatted before the search.
    /// If the patient is not found, a 404 response will be returned.
    /// </remarks>
    public async Task<ResponseBase<GetByCPFPacienteResponseItem>> Handle(GetByCPFPacienteQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Starting to fetch paciente with CPF: {request.CPF}");

            var paciente = await _pacienteService.FindByAsync(x => x.CPF == request.FormattedCPF);

            var responseItem = _mapper.Map<GetByCPFPacienteResponseItem>(paciente);

            return new ResponseBase<GetByCPFPacienteResponseItem>(responseItem)
            {
                IsSuccessful = true,
                Message = $"Successfully retrieved paciente with CPF: {request.CPF}",
                StatusCode = StatusCodes.Status200OK
            };
        }
        catch (KeyNotFoundException)
        {
            _logger.LogWarning($"Paciente with CPF: {request.CPF} not found");
            return new ResponseBase<GetByCPFPacienteResponseItem>(null)
            {
                IsSuccessful = false,
                Message = $"Paciente with CPF: {request.CPF} not found",
                StatusCode = StatusCodes.Status404NotFound
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error while fetching paciente with CPF: {request.CPF}");
            return new ResponseBase<GetByCPFPacienteResponseItem>(null)
            {
                IsSuccessful = false,
                Message = "An error occurred while processing your request. Please try again later.",
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
