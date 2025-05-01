using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
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

    public async Task<Paciente> GetByIdAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Starting to fetch paciente with ID: {Id}", id);

            var response = await _pacienteRepository.GetByIdAsync(id);
            if (response == null)
            {
                _logger.LogWarning("Paciente with ID: {Id} not found", id);
                throw new KeyNotFoundException($"Paciente with ID: {id} not found.");
            }

            _logger.LogInformation("Successfully retrieved paciente with ID: {Id}", id);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching paciente with ID: {Id}", id);
            throw new Exception("Error fetching paciente. Please try again later.", ex);
        }
    }

    public async Task<IEnumerable<Paciente>> FindAllAsync(Expression<Func<Paciente, bool>> predicate)
    {
        try
        {
            _logger.LogInformation("Starting to find all pacientes.");

            var response = await _pacienteRepository.FindAsync(predicate);
            if (response == null)
            {
                _logger.LogWarning("No pacientes found matching the criteria.");
                throw new KeyNotFoundException("No pacientes found matching the criteria.");
            }

            _logger.LogInformation($"Successfully found {response.Count()} pacientes matching the criteria.");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error finding pacientes matching the criteria.");
            throw new Exception("Error finding pacientes. Please try again later.", ex);
        }
    }

    public async Task<Paciente> FindByAsync(Expression<Func<Paciente, bool>> predicate)
    {
        try
        {
            _logger.LogInformation("Starting to find paciente matching the criteria.");

            var response = await _pacienteRepository.FindAsync(predicate);
            var paciente = response.FirstOrDefault();

            if (paciente == null)
            {
                _logger.LogWarning("No paciente found matching the criteria.");
                throw new KeyNotFoundException("No paciente found matching the criteria.");
            }

            _logger.LogInformation("Successfully found paciente matching the criteria.");

            return paciente;
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error finding paciente matching the criteria.");
            throw new Exception("Error finding paciente. Please try again later.", ex);
        }
    }
}

