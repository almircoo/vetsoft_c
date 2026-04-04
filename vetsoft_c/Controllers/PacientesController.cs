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
        public async Task<ActionResult<PacienteResponseDTO>> Crear([FromBody] PacienteCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var codigo = await _codigoService.GenerarCodigoPacienteAsync();

                var paciente = new Paciente
                {
                    Codigo = codigo,
                    Nombre = dto.Nombre,
                    Especie = dto.Especie,
                    Raza = dto.Raza,
                    Edad = dto.Edad,
                    Peso = dto.Peso,
                    Color = dto.Color,
                    Alergias = dto.Alergias,
                    IdCliente = dto.ClienteId,
                    Estado = true
                };

                var creado = await _pacienteService.Crear(paciente);

                var response = new PacienteResponseDTO
                {
                    Id = creado.IdPaciente,
                    Codigo = creado.Codigo,
                    Nombre = creado.Nombre,
                    Especie = creado.Especie,
                    Raza = creado.Raza,
                    Edad = creado.Edad,
                    Peso = creado.Peso,
                    Color = creado.Color,
                    Alergias = creado.Alergias,
                    Estado = creado.Estado,
                    ClienteId = creado.IdCliente
                };

                return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.IdPaciente }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear paciente", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PacienteResponseDTO>> Actualizar(long id, [FromBody] PacienteUpdateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var pacienteExistente = await _pacienteService.ObtenerPorId(id);
                if (pacienteExistente == null)
                    return NotFound(new { message = "Paciente no encontrado" });

                var pacienteActualizar = new Paciente
                {
                    IdPaciente = id,
                    Codigo = pacienteExistente.Codigo,
                    Nombre = dto.Nombre ?? pacienteExistente.Nombre,
                    Especie = dto.Especie ?? pacienteExistente.Especie,
                    Raza = dto.Raza ?? pacienteExistente.Raza,
                    Edad = dto.Edad ?? pacienteExistente.Edad,
                    Peso = dto.Peso ?? pacienteExistente.Peso,
                    Color = dto.Color ?? pacienteExistente.Color,
                    Alergias = dto.Alergias ?? pacienteExistente.Alergias,
                    IdCliente = dto.ClienteId ?? pacienteExistente.IdCliente,
                    Estado = dto.Estado ?? pacienteExistente.Estado
                };

                var actualizado = await _pacienteService.Actualizar(id, pacienteActualizar);

                var response = new PacienteResponseDTO
                {
                    Id = actualizado.IdPaciente,
                    Codigo = actualizado.Codigo,
                    Nombre = actualizado.Nombre,
                    Especie = actualizado.Especie,
                    Raza = actualizado.Raza,
                    Edad = actualizado.Edad,
                    Peso = actualizado.Peso,
                    Color = actualizado.Color,
                    Alergias = actualizado.Alergias,
                    Estado = actualizado.Estado,
                    ClienteId = actualizado.IdCliente
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar paciente", error = ex.Message });
            }
        }

        // [HttpPost]
        // public async Task<ActionResult<Paciente>> Crear([FromBody] Paciente paciente)
        // {
        //     var creado = await _pacienteService.Crear(paciente);
        //     return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.IdPaciente }, creado);
        // }

        // [HttpPut("{id}")]
        // public async Task<ActionResult<Paciente>> Actualizar(long id, [FromBody] Paciente paciente)
        // {
        //     try
        //     {
        //         return Ok(await _pacienteService.Actualizar(id, paciente));
        //     }
        //     catch
        //     {
        //         return NotFound();
        //     }
        // }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            await _pacienteService.Eliminar(id);
            return NoContent();
        }
    }
}
