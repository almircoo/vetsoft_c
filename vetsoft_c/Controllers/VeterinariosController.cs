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
    public class VeterinariosController : ControllerBase
    {
        private readonly VeterinarioService _veterinarioService;
        private readonly CodigoService _codigoService;

        public VeterinariosController(VeterinarioService veterinarioService, CodigoService codigoService)
        {
            _veterinarioService = veterinarioService;
            _codigoService = codigoService;
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
        public async Task<ActionResult<VeterinarioResponseDTO>> Crear([FromBody] VeterinarioCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var codigo = await _codigoService.GenerarCodigoVeterinarioAsync();

                var veterinario = new Veterinario
                {
                    Codigo = codigo,
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Correo = dto.Correo,
                    Telefono = dto.Telefono,
                    Especialidad = dto.Especialidad,
                    NumeroColegiado = dto.NumeroColegiado,
                    Estado = true
                };

                var creado = await _veterinarioService.Crear(veterinario);

                var response = new VeterinarioResponseDTO
                {
                    Id = creado.IdVeterinario,
                    Codigo = creado.Codigo,
                    Nombre = creado.Nombre,
                    Apellido = creado.Apellido,
                    Correo = creado.Correo,
                    Telefono = creado.Telefono,
                    Especialidad = creado.Especialidad,
                    NumeroColegiado = creado.NumeroColegiado,
                    Estado = creado.Estado
                };

                return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.IdVeterinario }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear veterinario", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VeterinarioResponseDTO>> Actualizar(long id, [FromBody] VeterinarioUpdateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var veterinarioExistente = await _veterinarioService.ObtenerPorId(id);
                if (veterinarioExistente == null)
                    return NotFound(new { message = "Veterinario no encontrado" });

                var veterinarioActualizar = new Veterinario
                {
                    IdVeterinario = id,
                    Codigo = veterinarioExistente.Codigo,
                    Nombre = dto.Nombre ?? veterinarioExistente.Nombre,
                    Apellido = dto.Apellido ?? veterinarioExistente.Apellido,
                    Correo = dto.Correo ?? veterinarioExistente.Correo,
                    Telefono = dto.Telefono ?? veterinarioExistente.Telefono,
                    Especialidad = dto.Especialidad ?? veterinarioExistente.Especialidad,
                    NumeroColegiado = dto.NumeroColegiado ?? veterinarioExistente.NumeroColegiado,
                    Estado = dto.Estado ?? veterinarioExistente.Estado
                };

                var actualizado = await _veterinarioService.Actualizar(id, veterinarioActualizar);

                var response = new VeterinarioResponseDTO
                {
                    Id = actualizado.IdVeterinario,
                    Codigo = actualizado.Codigo,
                    Nombre = actualizado.Nombre,
                    Apellido = actualizado.Apellido,
                    Correo = actualizado.Correo,
                    Telefono = actualizado.Telefono,
                    Especialidad = actualizado.Especialidad,
                    NumeroColegiado = actualizado.NumeroColegiado,
                    Estado = actualizado.Estado
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar veterinario", error = ex.Message });
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
