using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetsoft_c.Data;
using vetsoft_c.Models;

namespace vetsoft_c.Services
{
    public class VeterinarioService
    {
        private readonly AppDbContext _context;

        public VeterinarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Veterinario>> ObtenerTodos()
        {
            return await _context.Veterinarios.ToListAsync();
        }

        public async Task<List<Veterinario>> BusquedaGlobal(string? termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
            {
                return await _context.Veterinarios.ToListAsync();
            }

            string t = termino.ToLower();
            
            return await _context.Veterinarios
                .Where(v => 
                    v.Codigo.ToLower().Contains(t) ||
                    v.Nombre.ToLower().Contains(t) ||
                    v.Apellido.ToLower().Contains(t) ||
                    (v.NumeroColegiado != null && v.NumeroColegiado.ToLower().Contains(t)))
                .ToListAsync();
        }

        public async Task<Veterinario?> ObtenerPorId(long id)
        {
            return await _context.Veterinarios.FindAsync(id);
        }

        public async Task<Veterinario> Crear(Veterinario veterinario)
        {
            _context.Veterinarios.Add(veterinario);
            await _context.SaveChangesAsync();
            return veterinario;
        }

        public async Task<Veterinario> Actualizar(long id, Veterinario actualizado)
        {
            var v = await _context.Veterinarios.FindAsync(id);
            if (v == null) throw new Exception("No encontrado");

            v.Nombre = actualizado.Nombre ?? v.Nombre;
            v.Apellido = actualizado.Apellido ?? v.Apellido;
            v.NumeroColegiado = actualizado.NumeroColegiado ?? v.NumeroColegiado;
            v.Especialidad = actualizado.Especialidad ?? v.Especialidad;
            v.Correo = actualizado.Correo ?? v.Correo;
            v.Telefono = actualizado.Telefono ?? v.Telefono;
            v.Estado = actualizado.Estado;

            await _context.SaveChangesAsync();
            return v;
        }

        public async Task Eliminar(long id)
        {
            var v = await _context.Veterinarios.FindAsync(id);
            if (v != null)
            {
                v.Estado = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
