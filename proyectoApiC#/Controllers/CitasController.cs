using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using proyectoApiC_.Models;
using proyectoApiC_.Services;

namespace proyectoApiC_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly CitaService _citaService;

        public CitasController(CitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cita>>> ObtenerTodos()
        {
            return Ok(await _citaService.ObtenerTodos());
        }

        [HttpGet("buscar/{termino}")]
        public async Task<ActionResult<List<Cita>>> BuscarGlobal(string termino)
        {
            return Ok(await _citaService.BusquedaGlobal(termino));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> ObtenerPorId(long id)
        {
            var c = await _citaService.ObtenerPorId(id);
            return c != null ? Ok(c) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Cita>> Crear([FromBody] Cita cita)
        {
            var creado = await _citaService.Crear(cita);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.IdCita }, creado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cita>> Actualizar(long id, [FromBody] Cita cita)
        {
            try
            {
                return Ok(await _citaService.Actualizar(id, cita));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            await _citaService.Eliminar(id);
            return NoContent();
        }
    }
}
