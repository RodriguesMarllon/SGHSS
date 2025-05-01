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

namespace Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("[controller]")]
    public class PacienteController : BaseController
    {
        private readonly IMapper _mapper;

        public PacienteController(IMapper mapper, IMediator mediator) 
            : base(mediator)
        {
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreatePacienteBodyModel body)
        {
            var queryMediator = new CreatePacienteBodyRequest { Pacientes = _mapper.Map<CreatePacienteDTO>(body)};            
            var response = await _mediator.Send(queryMediator);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            GetAllPacienteQueryRequest queryMediator = new();
            var response = await _mediator.Send(queryMediator);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdPacienteQueryModel query)
        {
            var queryMediator = _mapper.Map<GetByIdPacienteQueryRequest>(query);
            var response = await _mediator.Send(queryMediator);
            return StatusCode(response.StatusCode, response);
        }
        
    }
}
