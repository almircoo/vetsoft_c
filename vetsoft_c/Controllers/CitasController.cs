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
    public class CitasController : ControllerBase
    {
        private readonly CitaService _citaService;
        private readonly CodigoService _codigoService;

        public CitasController(CitaService citaService, CodigoService codigoService)
        {
            _citaService = citaService;
            _codigoService = codigoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CitaResponseDTO>>> ObtenerTodos()
            => Ok(await _citaService.ObtenerTodos());

        [HttpGet("buscar/{termino}")]
        public async Task<ActionResult<List<CitaResponseDTO>>> BuscarGlobal(string termino)
            => Ok(await _citaService.BusquedaGlobal(termino));

        [HttpGet("{id}")]
        public async Task<ActionResult<CitaResponseDTO>> ObtenerPorId(long id)
        {
            var c = await _citaService.ObtenerPorId(id);
            return c != null ? Ok(c) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CitaResponseDTO>> Crear([FromBody] CitaCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var cita = new Cita
                {
                    Codigo = await _codigoService.GenerarCodigoCitaAsync(),
                    FechaHora = dto.FechaHora,
                    Motivo = dto.Motivo,
                    IdPaciente = dto.PacienteId,
                    IdVeterinario = dto.VeterinarioId,
                    IdServicio = dto.ServicioId,
                    Estado = "PENDIENTE"
                };

                var creado = await _citaService.Crear(cita);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.IdCita },
                    await _citaService.ObtenerPorId(creado.IdCita));
            }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { message = "Error al crear cita", error = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CitaResponseDTO>> Actualizar(long id, [FromBody] CitaUpdateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existente = await _citaService.ObtenerPorId(id);
                if (existente == null) return NotFound(new { message = "Cita no encontrada" });

                var cita = new Cita
                {
                    IdCita = id,
                    Codigo = existente.Codigo,
                    FechaHora = dto.FechaHora ?? existente.FechaHora,
                    Motivo = dto.Motivo ?? existente.Motivo,
                    Notas = dto.Notas ?? existente.Notas,
                    Diagnostico = dto.Diagnostico ?? existente.Diagnostico,
                    Tratamiento = dto.Tratamiento ?? existente.Tratamiento,
                    Estado = dto.Estado ?? existente.Estado,
                    IdPaciente = existente.PacienteId,
                    IdVeterinario = existente.VeterinarioId,
                    IdServicio = existente.ServicioId
                };

                await _citaService.Actualizar(id, cita);
                return Ok(await _citaService.ObtenerPorId(id));
            }
            catch (Exception ex) { return StatusCode(500, new { message = "Error al actualizar cita", error = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            await _citaService.Eliminar(id);
            return NoContent();
        }
    }
}
