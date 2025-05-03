using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Api.Controllers.Abstracts;
using System.Diagnostics.CodeAnalysis;
using Application.DTOs.Pacientes;
using Api.Models.Pacientes;
using Application.Handlers.Pacientes.RequestBody.Create;
using Application.Handlers.Pacientes.Queries.GetAll;
using Application.Handlers.Pacientes.Queries.GetById;
using Application.Handlers.Pacientes.Queries.GetByCPF;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers
{
    /// <summary>
    /// Controller for managing patient operations
    /// </summary>
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class PacienteController : BaseController
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the PacienteController class
        /// </summary>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        /// <param name="mediator">MediatR instance for handling requests</param>
        public PacienteController(IMapper mapper, IMediator mediator) 
            : base(mediator)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new patient
        /// </summary>
        /// <param name="body">Patient data to be created</param>
        /// <returns>Created patient information</returns>
        /// <response code="201">Returns the newly created patient</response>
        /// <response code="400">If the patient data is invalid</response>
        /// <response code="500">If there was an internal server error</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /paciente/create
        ///     {
        ///         "name": "John Doe",
        ///         "cpf": "12345678900",
        ///         "birthDate": "1990-01-01",
        ///         "phone": "(11) 99999-9999",
        ///         "email": "john.doe@example.com",
        ///         "address": "123 Main St, City, State, 12345"
        ///     }
        ///
        /// </remarks>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreatePacienteBodyModel body)
        {
            var queryMediator = new CreatePacienteBodyRequest { Pacientes = _mapper.Map<CreatePacienteDTO>(body)};            
            var response = await _mediator.Send(queryMediator);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Gets all patients
        /// </summary>
        /// <returns>List of all patients</returns>
        /// <response code="200">Returns the list of patients</response>
        /// <response code="500">If there was an internal server error</response>
        /// <remarks>
        /// Returns a list of all registered patients in the system.
        /// </remarks>
        [HttpGet("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            GetAllPacienteQueryRequest queryMediator = new();
            var response = await _mediator.Send(queryMediator);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Gets a patient by ID
        /// </summary>
        /// <param name="query">Patient ID</param>
        /// <returns>Patient information</returns>
        /// <response code="200">Returns the patient</response>
        /// <response code="404">If the patient is not found</response>
        /// <response code="500">If there was an internal server error</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /paciente/get-by-id?id=123e4567-e89b-12d3-a456-426614174000
        ///
        /// </remarks>
        [HttpGet("get-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromQuery] GetByIdPacienteQueryModel query)
        {
            var queryMediator = _mapper.Map<GetByIdPacienteQueryRequest>(query);
            var response = await _mediator.Send(queryMediator);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Gets a patient by CPF
        /// </summary>
        /// <param name="query">Patient CPF</param>
        /// <returns>Patient information</returns>
        /// <response code="200">Returns the patient</response>
        /// <response code="404">If the patient is not found</response>
        /// <response code="400">If the CPF is invalid</response>
        /// <response code="500">If there was an internal server error</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /paciente/get-by-cpf?cpf=12345678900
        ///
        /// </remarks>
        [HttpGet("get-by-cpf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByCPF([FromQuery] GetByCPFPacienteQueryModel query)
        {
            var queryMediator = _mapper.Map<GetByCPFPacienteQueryRequest>(query);
            var response = await _mediator.Send(queryMediator);
            return StatusCode(response.StatusCode, response);
        }
    }
}
