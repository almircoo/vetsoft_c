using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetsoft_c.Data;
using vetsoft_c.Models;

namespace vetsoft_c.Services
{
    public class ServicioService
    {
        private readonly AppDbContext _context;

        public ServicioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Servicio>> ObtenerTodos()
        {
            return await _context.Servicios.ToListAsync();
        }

        public async Task<List<Servicio>> BusquedaGlobal(string? termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
            {
                return await _context.Servicios.ToListAsync();
            }

            string t = termino.ToLower();
            
            return await _context.Servicios
                .Where(s => 
                    s.Codigo.ToLower().Contains(t) ||
                    s.Nombre.ToLower().Contains(t) ||
                    (s.Descripcion != null && s.Descripcion.ToLower().Contains(t)) ||
                    (s.DuracionEstimada != null && s.DuracionEstimada.ToString()!.Contains(t)) ||
                    s.Precio.ToString().Contains(t) ||
                    (t == "activo" && s.Estado == true) ||
                    (t == "inactivo" && s.Estado == false))
                .ToListAsync();
        }

        public async Task<List<Servicio>> ObtenerActivos()
        {
            return await _context.Servicios
                .Where(s => s.Estado == true)
                .OrderBy(s => s.Nombre)
                .ToListAsync();
        }

        public async Task<Servicio?> ObtenerPorId(long id)
        {
            return await _context.Servicios.FindAsync(id);
        }

        public async Task<List<Servicio>> BuscarPorNombre(string nombre)
        {
            return await _context.Servicios
                .Where(s => s.Nombre.ToLower().Contains(nombre.ToLower()))
                .ToListAsync();
        }

        public async Task<List<Servicio>> BuscarPorRangoPrecio(double min, double max)
        {
            return await _context.Servicios
                .Where(s => s.Precio >= min && s.Precio <= max && s.Estado == true)
                .ToListAsync();
        }

        public async Task<Servicio> Crear(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();
            return servicio;
        }

        public async Task<Servicio> Actualizar(long id, Servicio actualizado)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null) throw new Exception("No encontrado");

            servicio.Nombre = actualizado.Nombre ?? servicio.Nombre;
            servicio.Descripcion = actualizado.Descripcion ?? servicio.Descripcion;
            servicio.Precio = actualizado.Precio != 0 ? actualizado.Precio : servicio.Precio;
            servicio.DuracionEstimada = actualizado.DuracionEstimada ?? servicio.DuracionEstimada;
            servicio.Estado = actualizado.Estado;

            await _context.SaveChangesAsync();
            return servicio;
        }

        public async Task Eliminar(long id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio != null)
            {
                servicio.Estado = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<long> ContarActivos()
        {
            return await _context.Servicios.CountAsync(s => s.Estado == true);
        }
    }
}
