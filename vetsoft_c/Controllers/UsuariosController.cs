using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vetsoft_c.DTOs;
using vetsoft_c.Models;
using vetsoft_c.Services;

namespace vetsoft_c.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly CodigoService _codigoService;

        public UsuariosController(UsuarioService usuarioService, CodigoService codigoService)
        {
            _usuarioService = usuarioService;
            _codigoService = codigoService;
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
        public async Task<ActionResult<UsuarioResponseDTO>> Crear([FromBody] UsuarioCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var codigo = await _codigoService.GenerarCodigoUsuarioAsync();

                var usuario = new Usuario
                {
                    Codigo = codigo,
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Correo = dto.Correo,
                    Contrasena = _hashPassword(dto.Contrasena),
                    RolString = dto.Rol,
                    Estado = true
                };

                var creado = await _usuarioService.Crear(usuario);

                var response = new UsuarioResponseDTO
                {
                    IdUsuario = creado.IdUsuario,
                    Codigo = creado.Codigo,
                    Nombre = creado.Nombre,
                    Apellido = creado.Apellido,
                    Correo = creado.Correo,
                    Rol = creado.RolString,
                    Estado = creado.Estado
                };

                return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.IdUsuario }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear usuario", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioResponseDTO>> Actualizar(long id, [FromBody] UsuarioUpdateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var usuarioExistente = await _usuarioService.ObtenerPorId(id);
                if (usuarioExistente == null)
                    return NotFound(new { message = "Usuario no encontrado" });

                var usuarioActualizar = new Usuario
                {
                    IdUsuario = id,
                    Codigo = usuarioExistente.Codigo,
                    Nombre = dto.Nombre ?? usuarioExistente.Nombre,
                    Apellido = dto.Apellido ?? usuarioExistente.Apellido,
                    Correo = dto.Correo ?? usuarioExistente.Correo,
                    Contrasena = string.IsNullOrEmpty(dto.Contrasena) ? usuarioExistente.Contrasena : _hashPassword(dto.Contrasena),
                    RolString = dto.Rol ?? usuarioExistente.RolString,
                    Estado = dto.Estado ?? usuarioExistente.Estado
                };

                var actualizado = await _usuarioService.Actualizar(id, usuarioActualizar);

                var response = new UsuarioResponseDTO
                {
                    IdUsuario = actualizado.IdUsuario,
                    Codigo = actualizado.Codigo,
                    Nombre = actualizado.Nombre,
                    Apellido = actualizado.Apellido,
                    Correo = actualizado.Correo,
                    Rol = actualizado.RolString,
                    Estado = actualizado.Estado
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar usuario", error = ex.Message });
            }
        }

        private string _hashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return System.Convert.ToBase64String(hashedBytes);
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
