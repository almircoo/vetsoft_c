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

        public ServiciosController(ServicioService servicioService)
        {
            _servicioService = servicioService;
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
        public async Task<ActionResult<Servicio>> Crear([FromBody] Servicio servicio)
        {
            var creado = await _servicioService.Crear(servicio);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.IdServicio }, creado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Servicio>> Actualizar(long id, [FromBody] Servicio servicio)
        {
            try
            {
                return Ok(await _servicioService.Actualizar(id, servicio));
            }
            catch
            {
                return NotFound();
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
