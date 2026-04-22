using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using proyectoApiC_.Models;
using proyectoApiC_.Services;

namespace proyectoApiC_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly PacienteService _pacienteService;

        public PacientesController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Paciente>>> ObtenerTodos()
        {
            return Ok(await _pacienteService.ObtenerTodos());
        }

        [HttpGet("buscar/{termino}")]
        public async Task<ActionResult<List<Paciente>>> BuscarGlobal(string termino)
        {
            return Ok(await _pacienteService.BusquedaGlobal(termino));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> ObtenerPorId(long id)
        {
            var p = await _pacienteService.ObtenerPorId(id);
            return p != null ? Ok(p) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Paciente>> Crear([FromBody] Paciente paciente)
        {
            var creado = await _pacienteService.Crear(paciente);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.IdPaciente }, creado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Paciente>> Actualizar(long id, [FromBody] Paciente paciente)
        {
            try
            {
                return Ok(await _pacienteService.Actualizar(id, paciente));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            await _pacienteService.Eliminar(id);
            return NoContent();
        }
    }
}
