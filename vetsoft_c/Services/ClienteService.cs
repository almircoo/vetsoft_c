using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vetsoft_c.Models;
using vetsoft_c.Repositories;

namespace vetsoft_c.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<List<Cliente>> ObtenerTodos()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return clientes.ToList();
        }

        public async Task<List<Cliente>> BusquedaGlobal(string? termino)
        {
            var clientes = await _clienteRepository.GetAllAsync();

            if (string.IsNullOrWhiteSpace(termino))
            {
                return clientes.ToList();
            }

            string t = termino.ToLower();

            return clientes.Where(c =>
                c.Codigo.ToLower().Contains(t) ||
                c.Nombre.ToLower().Contains(t) ||
                c.Apellido.ToLower().Contains(t) ||
                (c.Ciudad != null && c.Ciudad.ToLower().Contains(t)) ||
                (c.Direccion != null && c.Direccion.ToLower().Contains(t)) ||
                (c.Correo != null && c.Correo.ToLower().Contains(t)) ||
                (c.Telefono != null && c.Telefono.Contains(t)) ||
                (t == "activo" && c.Estado == true) ||
                (t == "inactivo" && c.Estado == false))
            .ToList();
        }

        public async Task<List<Cliente>> ObtenerActivos()
        {
            var activos = await _clienteRepository.GetByEstadoAsync(true);
            return activos.OrderBy(c => c.Nombre).ToList();
        }

        public async Task<Cliente?> ObtenerPorCodigo(string codigo)
        {
            var resultados = await _clienteRepository.GetByCodigoAsync(codigo);
            return resultados.FirstOrDefault();
        }

        public async Task<Cliente?> ObtenerPorId(long id)
        {
            return await _clienteRepository.GetByIdAsync((int)id);
        }

        public async Task<List<Cliente>> BuscarPorNombre(string nombre)
        {
            var resultados = await _clienteRepository.GetByNombreAsync(nombre);
            return resultados.ToList();
        }

        public async Task<List<Cliente>> BuscarPorCiudad(string ciudad)
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return clientes
                .Where(c => c.Ciudad != null && c.Ciudad.ToLower() == ciudad.ToLower())
                .ToList();
        }

        public async Task<Cliente?> ObtenerPorCorreo(string correo)
        {
            return await _clienteRepository.GetByCorreoAsync(correo);
        }

        public async Task<Cliente?> ObtenerPorTelefono(string telefono)
        {
            return await _clienteRepository.GetByTelefonoAsync(telefono);
        }

        public async Task<Cliente> Crear(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre)) throw new ArgumentException("El nombre es requerido");
            if (string.IsNullOrWhiteSpace(cliente.Apellido)) throw new ArgumentException("El apellido es requerido");

            if (!string.IsNullOrWhiteSpace(cliente.Correo))
            {
                var existe = await _clienteRepository.AnyAsync(c => c.Correo == cliente.Correo);
                if (existe) throw new ArgumentException("El correo ya existe");
            }

            var creado = await _clienteRepository.AddAsync(cliente);
            await _clienteRepository.SaveChangesAsync();
            return creado;
        }

        public async Task<Cliente> Actualizar(long id, Cliente clienteActualizado)
        {
            var cliente = await _clienteRepository.GetByIdAsync((int)id);
            if (cliente == null) throw new Exception("Cliente no encontrado");

            if (clienteActualizado.Nombre != null) cliente.Nombre = clienteActualizado.Nombre;
            if (clienteActualizado.Apellido != null) cliente.Apellido = clienteActualizado.Apellido;
            if (clienteActualizado.Correo != null) cliente.Correo = clienteActualizado.Correo;
            if (clienteActualizado.Telefono != null) cliente.Telefono = clienteActualizado.Telefono;
            if (clienteActualizado.Direccion != null) cliente.Direccion = clienteActualizado.Direccion;
            if (clienteActualizado.Ciudad != null) cliente.Ciudad = clienteActualizado.Ciudad;

            cliente.Estado = clienteActualizado.Estado;

            await _clienteRepository.UpdateAsync(cliente);
            await _clienteRepository.SaveChangesAsync();
            return cliente;
        }

        public async Task Eliminar(long id)
        {
            var cliente = await _clienteRepository.GetByIdAsync((int)id);
            if (cliente != null)
            {
                cliente.Estado = false;
                await _clienteRepository.UpdateAsync(cliente);
                await _clienteRepository.SaveChangesAsync();
            }
        }

        public async Task EliminarCliente(long id)
        {
            await _clienteRepository.DeleteAsync((int)id);
            await _clienteRepository.SaveChangesAsync();
        }

        public async Task<long> ContarActivos()
        {
            return await _clienteRepository.CountAsync(c => c.Estado == true);
        }
    }
}
