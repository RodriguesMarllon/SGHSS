using Application.Models.Abstracts;
using Application.Models.Response.Pacientes;
using AutoMapper;
using Domain.Service.Pacientes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.Pacientes.Queries.GetByCPF;

public class GetByCPFPacienteQueryHandler : IRequestHandler<GetByCPFPacienteQueryRequest, ResponseBase<GetByCPFPacienteResponseItem>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IPacienteService _pacienteService;

    public GetByCPFPacienteQueryHandler(IMapper mapper, ILogger<GetByCPFPacienteQueryHandler> logger, IPacienteService pacienteService)
    {
        _mapper = mapper;
        _logger = logger;
        _pacienteService = pacienteService;
    }

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
