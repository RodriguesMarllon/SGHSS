using Application.Models.Abstracts;
using Application.Models.Response.Pacientes;
using AutoMapper;
using Domain.Service.Pacientes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.Pacientes.Queries.GetById;

public class GetByIdPacienteQueryHandler : IRequestHandler<GetByIdPacienteQueryRequest, ResponseBase<GetByIdPacienteResponseItem>>
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IPacienteService _pacienteService;

    public GetByIdPacienteQueryHandler(IMapper mapper, ILogger<GetByIdPacienteQueryHandler> logger, IPacienteService pacienteService)
    {
        _mapper = mapper;
        _logger = logger;
        _pacienteService = pacienteService;
    }

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
