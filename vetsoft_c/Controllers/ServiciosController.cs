using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vetsoft_c.Models;
using vetsoft_c.Services;

namespace vetsoft_c.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly ServicioService _servicioService;
        private readonly CodigoService _codigoService;

        public ServiciosController(ServicioService servicioService, CodigoService codigoService)
        {
            _servicioService = servicioService;
            _codigoService = codigoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Servicio>>> ObtenerTodos()
        {
            return Ok(await _servicioService.ObtenerTodos());
        }

        [HttpGet("buscar/{termino}")]
        public async Task<ActionResult<List<Servicio>>> BuscarGlobal(string termino)
        {
            return Ok(await _servicioService.BusquedaGlobal(termino));
        }

        [HttpGet("activos")]
        public async Task<ActionResult<List<Servicio>>> ObtenerActivos()
        {
            return Ok(await _servicioService.ObtenerActivos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Servicio>> ObtenerPorId(long id)
        {
            var servicio = await _servicioService.ObtenerPorId(id);
            return servicio != null ? Ok(servicio) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ServicioResponseDTO>> Crear([FromBody] ServicioCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var codigo = await _codigoService.GenerarCodigoServicioAsync();

                var servicio = new Servicio
                {
                    Codigo = codigo,
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    Precio = dto.Precio,
                    DuracionEstimada = dto.DuracionEstimada,
                    Estado = true
                };

                var creado = await _servicioService.Crear(servicio);

                var response = new ServicioResponseDTO
                {
                    IdServicio = creado.IdServicio,
                    Codigo = creado.Codigo,
                    Nombre = creado.Nombre,
                    Descripcion = creado.Descripcion,
                    Precio = creado.Precio,
                    DuracionEstimada = creado.DuracionEstimada,
                    Estado = creado.Estado
                };

                return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.IdServicio }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear servicio", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServicioResponseDTO>> Actualizar(long id, [FromBody] ServicioUpdateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var servicioExistente = await _servicioService.ObtenerPorId(id);
                if (servicioExistente == null)
                    return NotFound(new { message = "Servicio no encontrado" });

                var servicioActualizar = new Servicio
                {
                    IdServicio = id,
                    Codigo = servicioExistente.Codigo,
                    Nombre = dto.Nombre ?? servicioExistente.Nombre,
                    Descripcion = dto.Descripcion ?? servicioExistente.Descripcion,
                    Precio = dto.Precio ?? servicioExistente.Precio,
                    DuracionEstimada = dto.DuracionEstimada ?? servicioExistente.DuracionEstimada,
                    Estado = dto.Estado ?? servicioExistente.Estado
                };

                var actualizado = await _servicioService.Actualizar(id, servicioActualizar);

                var response = new ServicioResponseDTO
                {
                    IdServicio = actualizado.IdServicio,
                    Codigo = actualizado.Codigo,
                    Nombre = actualizado.Nombre,
                    Descripcion = actualizado.Descripcion,
                    Precio = actualizado.Precio,
                    DuracionEstimada = actualizado.DuracionEstimada,
                    Estado = actualizado.Estado
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar servicio", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            await _servicioService.Eliminar(id);
            return NoContent();
        }
    }
}
