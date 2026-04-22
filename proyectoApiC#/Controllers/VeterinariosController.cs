using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using proyectoApiC_.Models;
using proyectoApiC_.Services;

namespace proyectoApiC_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeterinariosController : ControllerBase
    {
        private readonly VeterinarioService _veterinarioService;

        public VeterinariosController(VeterinarioService veterinarioService)
        {
            _veterinarioService = veterinarioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Veterinario>>> ObtenerTodos()
        {
            return Ok(await _veterinarioService.ObtenerTodos());
        }

        [HttpGet("buscar/{termino}")]
        public async Task<ActionResult<List<Veterinario>>> BuscarGlobal(string termino)
        {
            return Ok(await _veterinarioService.BusquedaGlobal(termino));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Veterinario>> ObtenerPorId(long id)
        {
            var v = await _veterinarioService.ObtenerPorId(id);
            return v != null ? Ok(v) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Veterinario>> Crear([FromBody] Veterinario veterinario)
        {
            var creado = await _veterinarioService.Crear(veterinario);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.IdVeterinario }, creado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Veterinario>> Actualizar(long id, [FromBody] Veterinario veterinario)
        {
            try
            {
                return Ok(await _veterinarioService.Actualizar(id, veterinario));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            await _veterinarioService.Eliminar(id);
            return NoContent();
        }
    }
}
