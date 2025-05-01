using Domain.Entities.Pacientes;
using Domain.Interfaces.Repositories.Pacientes;
using Microsoft.Extensions.Logging;

namespace Domain.Service.Pacientes;

public class PacienteService : IPacienteService
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly ILogger _logger;

    public PacienteService(
        IPacienteRepository pacienteRepository, 
        ILogger<PacienteService> logger)
    {
        _pacienteRepository = pacienteRepository;
        _logger = logger;
    }

    public async Task<Paciente> CreateAsync(Paciente entity)
    {
        try
        {
            _logger.LogInformation("Starting Paciente creation with CPF: {CPF}", entity.CPF);
            
            await _pacienteRepository.CreateAsync(entity);
            _logger.LogInformation("Paciente created successfully. ID: {Id}", entity.Id);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating Paciente with CPF: {CPF}", entity.CPF);
            throw new Exception("Error creating Paciente. Please try again later.", ex);
        }
    }

    public async Task<IEnumerable<Paciente>> GetAllAsync()
    {
        try
        {
            _logger.LogInformation("Starting to fetch all pacientes");

            var response = await _pacienteRepository.GetAllAsync();
            _logger.LogInformation("Successfully retrieved {Count} pacientes", response.Count());

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all pacientes");
            throw new Exception("Error fetching all pacientes. Please try again later.", ex);
        }
    }
}

