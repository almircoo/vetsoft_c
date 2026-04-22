using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using proyectoApiC_.Models;
using proyectoApiC_.Services;

namespace proyectoApiC_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> ObtenerTodos()
        {
            var clientes = await _clienteService.ObtenerTodos();
            return Ok(clientes);
        }

        [HttpGet("buscar/{termino}")]
        public async Task<ActionResult<List<Cliente>>> Buscar(string termino)
        {
            var clientes = await _clienteService.BusquedaGlobal(termino);
            return Ok(clientes);
        }

        [HttpGet("activos")]
        public async Task<ActionResult<List<Cliente>>> ObtenerActivos()
        {
            var clientes = await _clienteService.ObtenerActivos();
            return Ok(clientes);
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<Cliente>> ObtenerPorId(string codigo)
        {
            var cliente = await _clienteService.ObtenerPorCodigo(codigo);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpGet("buscar/nombre")]
        public async Task<ActionResult<List<Cliente>>> BuscarPorNombre([FromQuery] string nombre)
        {
            var clientes = await _clienteService.BuscarPorNombre(nombre);
            return Ok(clientes);
        }

        [HttpGet("buscar/ciudad")]
        public async Task<ActionResult<List<Cliente>>> BuscarPorCiudad([FromQuery] string ciudad)
        {
            var clientes = await _clienteService.BuscarPorCiudad(ciudad);
            return Ok(clientes);
        }

        [HttpGet("correo/{correo}")]
        public async Task<ActionResult<Cliente>> ObtenerPorCorreo(string correo)
        {
            var cliente = await _clienteService.ObtenerPorCorreo(correo);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpGet("telefono/{telefono}")]
        public async Task<ActionResult<Cliente>> ObtenerPorTelefono(string telefono)
        {
            var cliente = await _clienteService.ObtenerPorTelefono(telefono);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Crear([FromBody] Cliente cliente)
        {
            try
            {
                var nuevoCliente = await _clienteService.Crear(cliente);
                return CreatedAtAction(nameof(ObtenerPorId), new { codigo = nuevoCliente.Codigo }, nuevoCliente);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cliente>> Actualizar(long id, [FromBody] Cliente cliente)
        {
            try
            {
                var clienteActualizado = await _clienteService.Actualizar(id, cliente);
                return Ok(clienteActualizado);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            try
            {
                await _clienteService.Eliminar(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("estadisticas/activos")]
        public async Task<ActionResult<long>> ContarActivos()
        {
            var total = await _clienteService.ContarActivos();
            return Ok(total);
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarCliente(long id)
        {
            await _clienteService.EliminarCliente(id);
            return Ok();
        }
    }
}
