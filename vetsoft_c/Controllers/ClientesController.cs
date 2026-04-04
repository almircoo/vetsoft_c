using System;
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
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        private readonly CodigoService _codigoService;

        public ClientesController(ClienteService clienteService, CodigoService codigoService)
        {
            _clienteService = clienteService;
            _codigoService = codigoService;
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
        public async Task<ActionResult<ClienteResponseDTO>> Crear([FromBody] ClienteCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Generar código único
                var codigo = await _codigoService.GenerarCodigoClienteAsync();

                // Crear cliente con código generado
                var cliente = new Cliente
                {
                    Codigo = codigo,
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Correo = dto.Correo,
                    Telefono = dto.Telefono,
                    Direccion = dto.Direccion,
                    Ciudad = dto.Ciudad,
                    Estado = true
                };

                var nuevoCliente = await _clienteService.Crear(cliente);

                var response = new ClienteResponseDTO
                {
                    Id = nuevoCliente.IdCliente,
                    Codigo = nuevoCliente.Codigo,
                    Nombre = nuevoCliente.Nombre,
                    Apellido = nuevoCliente.Apellido,
                    Correo = nuevoCliente.Correo,
                    Telefono = nuevoCliente.Telefono,
                    Direccion = nuevoCliente.Direccion,
                    Ciudad = nuevoCliente.Ciudad,
                    Estado = nuevoCliente.Estado
                };

                return CreatedAtAction(nameof(ObtenerPorId), new { codigo = nuevoCliente.Codigo }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear cliente", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteResponseDTO>> Actualizar(long id, [FromBody] ClienteUpdateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var clienteExistente = await _clienteService.ObtenerPorId(id);
                if (clienteExistente == null)
                    return NotFound(new { message = "Cliente no encontrado" });

                var clienteActualizar = new Cliente
                {
                    IdCliente = id,
                    Codigo = clienteExistente.Codigo,
                    Nombre = dto.Nombre ?? clienteExistente.Nombre,
                    Apellido = dto.Apellido ?? clienteExistente.Apellido,
                    Correo = dto.Correo ?? clienteExistente.Correo,
                    Telefono = dto.Telefono ?? clienteExistente.Telefono,
                    Direccion = dto.Direccion ?? clienteExistente.Direccion,
                    Ciudad = dto.Ciudad ?? clienteExistente.Ciudad,
                    Estado = dto.Estado ?? clienteExistente.Estado
                };

                var clienteActualizado = await _clienteService.Actualizar(id, clienteActualizar);

                var response = new ClienteResponseDTO
                {
                    Id = clienteActualizado.IdCliente,
                    Codigo = clienteActualizado.Codigo,
                    Nombre = clienteActualizado.Nombre,
                    Apellido = clienteActualizado.Apellido,
                    Correo = clienteActualizado.Correo,
                    Telefono = clienteActualizado.Telefono,
                    Direccion = clienteActualizado.Direccion,
                    Ciudad = clienteActualizado.Ciudad,
                    Estado = clienteActualizado.Estado
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar cliente", error = ex.Message });
            }
        }

        // [HttpPost]
        // public async Task<ActionResult<Cliente>> Crear([FromBody] Cliente cliente)
        // {
        //     try
        //     {
        //         var nuevoCliente = await _clienteService.Crear(cliente);
        //         return CreatedAtAction(nameof(ObtenerPorId), new { codigo = nuevoCliente.Codigo }, nuevoCliente);
        //     }
        //     catch (ArgumentException)
        //     {
        //         return BadRequest();
        //     }
        // }

        // [HttpPut("{id}")]
        // public async Task<ActionResult<Cliente>> Actualizar(long id, [FromBody] Cliente cliente)
        // {
        //     try
        //     {
        //         var clienteActualizado = await _clienteService.Actualizar(id, cliente);
        //         return Ok(clienteActualizado);
        //     }
        //     catch (Exception)
        //     {
        //         return NotFound();
        //     }
        // }

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
