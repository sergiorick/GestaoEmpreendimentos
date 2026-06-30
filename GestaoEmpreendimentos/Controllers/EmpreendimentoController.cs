using GestaoEmpreendimentos.Application.DTOs.Requests;
using GestaoEmpreendimentos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoEmpreendimentos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpreendimentosController : ControllerBase
    {
        private readonly IEmpreendimentoService _service;

        public EmpreendimentosController(IEmpreendimentoService service)
        {
            _service = service;
        }

        // POST: api/empreendimentos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmpreendimentoRequest request)
        {
            try
            {
                var response = await _service.CreateAsync(request);
                return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // GET: api/empreendimentos?nome=...&status=...&ordenarPor=...&pagina=1&tamanhoPagina=10
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? nome,
            [FromQuery] string? status,
            [FromQuery] string? ordenarPor,
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanhoPagina = 10)
        {
            var resultado = await _service.GetAllAsync(nome, status, ordenarPor, pagina, tamanhoPagina);
            return Ok(resultado);
        }

        // GET: api/empreendimentos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        // PUT: api/empreendimentos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmpreendimentoRequest request)
        {
            try
            {
                var response = await _service.UpdateAsync(id, request);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // PATCH: api/empreendimentos/{id}/inativar
        [HttpPatch("{id}/inativar")]
        public async Task<IActionResult> Inativar(Guid id)
        {
            try
            {
                await _service.InativarAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}