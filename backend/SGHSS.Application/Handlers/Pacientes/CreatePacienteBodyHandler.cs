using MediatR;
using Microsoft.Extensions.Logging;
using Application.Models.Response;
using AutoMapper;
using Application.Models.Abstracts;
using Microsoft.AspNetCore.Http;
using Domain.Entities.Pacientes;
using Domain.Service.Pacientes;
using FluentValidation;
using Application.Handlers.Pacientes.RequestBody.Create;
using Application.DTOs.Pacientes;

namespace Application.Handlers.Pacientes;

public class CreatePacienteBodyHandler : IRequestHandler<CreatePacienteBodyRequest, ResponseBase<CreatePacienteResponseItem>>
{
    private readonly IMapper _mapper;
    private readonly ILogger<CreatePacienteBodyHandler> _logger;
    private readonly IPacienteService _pacienteService;
    private readonly IValidator<CreatePacienteDTO> _validator;

    public CreatePacienteBodyHandler(
        IMapper mapper, 
        ILogger<CreatePacienteBodyHandler> logger, 
        IPacienteService pacienteService,
        IValidator<CreatePacienteDTO> validator)
    {
        _mapper = mapper;
        _logger = logger;
        _pacienteService = pacienteService;
        _validator = validator;
    }

    public async Task<ResponseBase<CreatePacienteResponseItem>> Handle(CreatePacienteBodyRequest request, CancellationToken cancellationToken)
    {
        try 
        {
            _logger.LogInformation("Starting patient creation request processing");

            // DTO validation
            var validationResult = await _validator.ValidateAsync(request.Pacientes);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Invalid patient data: {Errors}", 
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                
                return new ResponseBase<CreatePacienteResponseItem>(null)
                {
                    IsSuccessful = false,
                    Message = "Invalid data",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var paciente = await _pacienteService.CreateAsync(_mapper.Map<Paciente>(request.Pacientes));
            var responseItem = _mapper.Map<CreatePacienteResponseItem>(paciente);

            _logger.LogInformation("Patient created successfully. ID: {Id}", paciente.Id);

            return new ResponseBase<CreatePacienteResponseItem>(responseItem)   
            {
                IsSuccessful = true,
                Message = $"Patient {paciente.Name} has been created successfully.",
                StatusCode = StatusCodes.Status201Created
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while creating patient");
            return new ResponseBase<CreatePacienteResponseItem>(null)
            {
                IsSuccessful = false,
                Message = "An error occurred while processing your request. Please try again later.",
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
} 