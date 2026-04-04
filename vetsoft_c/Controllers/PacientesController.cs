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
    public class PacientesController : ControllerBase
    {
        private readonly PacienteService _pacienteService;
        private readonly CodigoService _codigoService;

        public PacientesController(PacienteService pacienteService, CodigoService codigoService)
        {
            _pacienteService = pacienteService;
            _codigoService = codigoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PacienteResponseDTO>>> ObtenerTodos()
            => Ok(await _pacienteService.ObtenerTodos());

        [HttpGet("buscar/{termino}")]
        public async Task<ActionResult<List<PacienteResponseDTO>>> BuscarGlobal(string termino)
            => Ok(await _pacienteService.BusquedaGlobal(termino));

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteResponseDTO>> ObtenerPorId(long id)
        {
            var p = await _pacienteService.ObtenerPorId(id);
            return p != null ? Ok(p) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PacienteResponseDTO>> Crear([FromBody] PacienteCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var paciente = new Paciente
                {
                    Codigo = await _codigoService.GenerarCodigoPacienteAsync(),
                    Nombre = dto.Nombre,
                    Especie = dto.Especie,
                    Raza = dto.Raza,
                    Edad = dto.Edad,
                    Peso = dto.Peso,
                    Color = dto.Color,
                    Alergias = dto.Alergias,
                    IdCliente = dto.IdCliente,
                    Estado = true
                };

                var creado = await _pacienteService.Crear(paciente);
                var response = await _pacienteService.ObtenerPorId(creado.IdPaciente);

                return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.IdPaciente }, response);
            }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { message = "Error al crear paciente", error = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PacienteResponseDTO>> Actualizar(long id, [FromBody] PacienteUpdateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existente = await _pacienteService.ObtenerPorId(id);
                if (existente == null) return NotFound(new { message = "Paciente no encontrado" });

                var paciente = new Paciente
                {
                    IdPaciente = id,
                    Codigo = existente.Codigo,
                    Nombre = dto.Nombre ?? existente.Nombre,
                    Especie = dto.Especie ?? existente.Especie,
                    Raza = dto.Raza ?? existente.Raza,
                    Edad = dto.Edad ?? existente.Edad,
                    Peso = dto.Peso ?? existente.Peso,
                    Color = dto.Color ?? existente.Color,
                    Alergias = dto.Alergias ?? existente.Alergias,
                    IdCliente = dto.IdCliente ?? existente.IdCliente,
                    Estado = dto.Estado ?? existente.Estado
                };

                await _pacienteService.Actualizar(id, paciente);
                return Ok(await _pacienteService.ObtenerPorId(id));
            }
            catch (Exception ex) { return StatusCode(500, new { message = "Error al actualizar paciente", error = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            await _pacienteService.Eliminar(id);
            return NoContent();
        }
    }
}
