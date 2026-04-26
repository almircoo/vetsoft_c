using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<CitaResponseDTO>> Crear([FromBody] CitaCreateDTO dto)
        {
            // var creado = await _citaService.Crear(cita);
            // return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.IdCita }, creado);
            try
                {
                  if (!ModelState.IsValid)
                      return BadRequest(ModelState);

                  var codigo = await _codigoService.GenerarCodigoCitaAsync();

                  var cita = new Cita
                  {
                      Codigo = codigo,
                      FechaHora = dto.FechaHora,
                      Motivo = dto.Motivo,
                      IdPaciente = dto.IdPaciente,
                      IdVeterinario = dto.IdVeterinario,
                      IdServicio = dto.IdServicio,
                      Estado = "PENDIENTE"
                  };

                  var creado = await _citaService.Crear(cita);

                  var response = new CitaResponseDTO
                  {
                      IdCita = creado.IdCita,
                      Codigo = creado.Codigo,
                      FechaHora = creado.FechaHora,
                      Motivo = creado.Motivo,
                      Diagnostico = creado.Diagnostico,
                      Tratamiento = creado.Tratamiento,
                      Notas = creado.Notas,
                      Estado = creado.Estado,
                      IdPaciente = creado.IdPaciente,
                      IdVeterinario = creado.IdVeterinario,
                      IdServicio = creado.IdServicio
                  };

                  return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.IdCita }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear cita", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CitaResponseDTO>> Actualizar(long id, [FromBody] CitaUpdateDTO dto)
        {
          try
          {
              if (!ModelState.IsValid)
                  return BadRequest(ModelState);

              var citaExistente = await _citaService.ObtenerPorId(id);
              if (citaExistente == null)
                  return NotFound(new { message = "Cita no encontrada" });

              var citaActualizar = new Cita
              {
                  IdCita = id,
                  Codigo = citaExistente.Codigo,
                  FechaHora = dto.FechaHora ?? citaExistente.FechaHora,
                  Motivo = dto.Motivo ?? citaExistente.Motivo,
                  Diagnostico = dto.Diagnostico ?? citaExistente.Diagnostico,
                  Tratamiento = dto.Tratamiento ?? citaExistente.Tratamiento,
                  Notas = dto.Notas ?? citaExistente.Notas,
                  Estado = dto.Estado ?? citaExistente.Estado,
                  IdPaciente = citaExistente.IdPaciente,
                  IdVeterinario = citaExistente.IdVeterinario,
                  IdServicio = citaExistente.IdServicio
              };

              var actualizado = await _citaService.Actualizar(id, citaActualizar);

              var response = new CitaResponseDTO
              {
                  IdCita = actualizado.IdCita,
                  Codigo = actualizado.Codigo,
                  FechaHora = actualizado.FechaHora,
                  Motivo = actualizado.Motivo,
                  Diagnostico = actualizado.Diagnostico,
                  Tratamiento = actualizado.Tratamiento,
                  Notas = actualizado.Notas,
                  Estado = actualizado.Estado,
                  IdPaciente = actualizado.IdPaciente,
                  IdVeterinario = actualizado.IdVeterinario,
                  IdServicio = actualizado.IdServicio
              };

              return Ok(response);
          }
          catch (Exception ex)
          {
              return StatusCode(500, new { message = "Error al actualizar cita", error = ex.Message });
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
