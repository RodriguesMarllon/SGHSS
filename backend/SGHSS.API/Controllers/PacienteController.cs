using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Api.Controllers.Abstracts;
using System.Diagnostics.CodeAnalysis;
using Application.DTOs.Pacientes;
using Api.Models.Pacientes;
using Domain.Service.Pacientes;
using Application.Handlers.Pacientes.RequestBody.Create;

namespace Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("[controller]")]
    public class PacienteController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IPacienteService _pacienteService;

        public PacienteController(IMapper mapper, IMediator mediator, IPacienteService pacienteService) 
            : base(mediator)
        {
            _mapper = mapper;
            _pacienteService = pacienteService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreatePacienteBodyModel body)
        {
            var queryMediator = new CreatePacienteBodyRequest { Pacientes = _mapper.Map<CreatePacienteDTO>(body)};            
            var response = await _mediator.Send(queryMediator);
            return StatusCode(response.StatusCode, response);
        }
    }
}
