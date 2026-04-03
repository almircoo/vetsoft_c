using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vetsoft_c.Models;
using vetsoft_c.Services;

namespace vetsoft_c.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> ObtenerTodos()
        {
            return Ok(await _usuarioService.ObtenerTodos());
        }

        [HttpGet("buscar/{termino}")]
        public async Task<ActionResult<List<Usuario>>> BuscarGlobal(string termino)
        {
            return Ok(await _usuarioService.BusquedaGlobal(termino));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> ObtenerPorId(long id)
        {
            var u = await _usuarioService.ObtenerPorId(id);
            return u != null ? Ok(u) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Crear([FromBody] Usuario usuario)
        {
            var creado = await _usuarioService.Crear(usuario);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.IdUsuario }, creado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Actualizar(long id, [FromBody] Usuario usuario)
        {
            try
            {
                return Ok(await _usuarioService.Actualizar(id, usuario));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            await _usuarioService.Eliminar(id);
            return NoContent();
        }
    }
}
