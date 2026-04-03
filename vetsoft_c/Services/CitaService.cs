using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetsoft_c.Data;
using vetsoft_c.Models;

namespace vetsoft_c.Services
{
    public class CitaService
    {
        private readonly AppDbContext _context;

        public CitaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cita>> ObtenerTodos()
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Veterinario)
                .Include(c => c.Servicio)
                .ToListAsync();
        }

        public async Task<List<Cita>> BusquedaGlobal(string? termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
            {
                return await _context.Citas
                    .Include(c => c.Paciente)
                    .Include(c => c.Veterinario)
                    .Include(c => c.Servicio)
                    .ToListAsync();
            }

            string t = termino.ToLower();
            
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Veterinario)
                .Include(c => c.Servicio)
                .Where(c => 
                    c.Codigo.ToLower().Contains(t) ||
                    (c.Paciente != null && c.Paciente.Nombre.ToLower().Contains(t)) ||
                    (c.Veterinario != null && c.Veterinario.Nombre.ToLower().Contains(t)) ||
                    (c.Veterinario != null && c.Veterinario.Apellido.ToLower().Contains(t)) ||
                    (c.Servicio != null && c.Servicio.Nombre.ToLower().Contains(t)))
                .ToListAsync();
        }

        public async Task<Cita?> ObtenerPorId(long id)
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Veterinario)
                .Include(c => c.Servicio)
                .FirstOrDefaultAsync(c => c.IdCita == id);
        }

        public async Task<Cita> Crear(Cita cita)
        {
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();
            return cita;
        }

        public async Task<Cita> Actualizar(long id, Cita actualizado)
        {
            var c = await _context.Citas.FindAsync(id);
            if (c == null) throw new Exception("No encontrado");

            c.FechaHora = actualizado.FechaHora;
            c.Motivo = actualizado.Motivo ?? c.Motivo;
            c.Notas = actualizado.Notas ?? c.Notas;
            c.Diagnostico = actualizado.Diagnostico ?? c.Diagnostico;
            c.Tratamiento = actualizado.Tratamiento ?? c.Tratamiento;
            c.Estado = actualizado.Estado ?? c.Estado;
            c.IdPaciente = actualizado.IdPaciente;
            c.IdVeterinario = actualizado.IdVeterinario;
            c.IdServicio = actualizado.IdServicio;

            await _context.SaveChangesAsync();
            return c;
        }

        public async Task Eliminar(long id)
        {
            var c = await _context.Citas.FindAsync(id);
            if (c != null)
            {
                c.Estado = "CANCELADA";
                await _context.SaveChangesAsync();
            }
        }
    }
}
