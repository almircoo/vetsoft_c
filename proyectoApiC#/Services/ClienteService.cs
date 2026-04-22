using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyectoApiC_.Data;
using proyectoApiC_.Models;

namespace proyectoApiC_.Services
{
    public class ClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> ObtenerTodos()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<List<Cliente>> BusquedaGlobal(string? termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
            {
                return await _context.Clientes.ToListAsync();
            }

            string t = termino.ToLower();
            
            return await _context.Clientes
                .Where(c => 
                    c.Codigo.ToLower().Contains(t) ||
                    c.Nombre.ToLower().Contains(t) ||
                    c.Apellido.ToLower().Contains(t) ||
                    (c.Ciudad != null && c.Ciudad.ToLower().Contains(t)) ||
                    (c.Direccion != null && c.Direccion.ToLower().Contains(t)) ||
                    (c.Correo != null && c.Correo.ToLower().Contains(t)) ||
                    (c.Telefono != null && c.Telefono.Contains(t)) ||
                    (t == "activo" && c.Estado == true) ||
                    (t == "inactivo" && c.Estado == false))
                .ToListAsync();
        }

        public async Task<List<Cliente>> ObtenerActivos()
        {
            return await _context.Clientes
                .Where(c => c.Estado == true)
                .OrderBy(c => c.Nombre)
                .ToListAsync();
        }

        public async Task<Cliente?> ObtenerPorCodigo(string codigo)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Codigo == codigo);
        }

        public async Task<List<Cliente>> BuscarPorNombre(string nombre)
        {
            return await _context.Clientes
                .Where(c => c.Nombre.ToLower().Contains(nombre.ToLower()))
                .ToListAsync();
        }

        public async Task<List<Cliente>> BuscarPorCiudad(string ciudad)
        {
            return await _context.Clientes
                .Where(c => c.Ciudad != null && c.Ciudad.ToLower() == ciudad.ToLower())
                .ToListAsync();
        }

        public async Task<Cliente?> ObtenerPorCorreo(string correo)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Correo == correo);
        }

        public async Task<Cliente?> ObtenerPorTelefono(string telefono)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Telefono == telefono);
        }

        public async Task<Cliente> Crear(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre)) throw new ArgumentException("El nombre es requerido");
            if (string.IsNullOrWhiteSpace(cliente.Apellido)) throw new ArgumentException("El apellido es requerido");

            if (!string.IsNullOrWhiteSpace(cliente.Correo))
            {
                var existe = await _context.Clientes.AnyAsync(c => c.Correo == cliente.Correo);
                if (existe) throw new ArgumentException("El correo ya existe");
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> Actualizar(long id, Cliente clienteActualizado)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) throw new Exception("Cliente no encontrado");

            if (clienteActualizado.Nombre != null) cliente.Nombre = clienteActualizado.Nombre;
            if (clienteActualizado.Apellido != null) cliente.Apellido = clienteActualizado.Apellido;
            if (clienteActualizado.Correo != null) cliente.Correo = clienteActualizado.Correo;
            if (clienteActualizado.Telefono != null) cliente.Telefono = clienteActualizado.Telefono;
            if (clienteActualizado.Direccion != null) cliente.Direccion = clienteActualizado.Direccion;
            if (clienteActualizado.Ciudad != null) cliente.Ciudad = clienteActualizado.Ciudad;
            
            // Assume you can update state
            cliente.Estado = clienteActualizado.Estado;

            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task Eliminar(long id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                cliente.Estado = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarCliente(long id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<long> ContarActivos()
        {
            return await _context.Clientes.CountAsync(c => c.Estado == true);
        }
    }
}
