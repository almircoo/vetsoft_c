using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vetsoft_c.Data;
using vetsoft_c.DTOs;
using vetsoft_c.Models;

namespace vetsoft_c.Services
{
    public class CitaService
    {
        private readonly AppDbContext _context;
        public CitaService(AppDbContext context) => _context = context;

        public async Task<List<CitaResponseDTO>> ObtenerTodos()
        {
            return await _context.Citas
                .AsNoTracking()
                .Select(c => ToDTO(c))
                .ToListAsync();
        }

        public async Task<List<CitaResponseDTO>> BusquedaGlobal(string? termino)
        {
            var query = _context.Citas.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(termino))
            {
                string t = termino.ToLower();
                query = query.Where(c =>
                    c.Codigo.ToLower().Contains(t) ||
                    c.Paciente!.Nombre.ToLower().Contains(t) ||
                    c.Veterinario!.Nombre.ToLower().Contains(t) ||
                    c.Veterinario!.Apellido.ToLower().Contains(t) ||
                    c.Servicio!.Nombre.ToLower().Contains(t));
            }

            return await query.Select(c => ToDTO(c)).ToListAsync();
        }

        public async Task<CitaResponseDTO?> ObtenerPorId(long id)
        {
            return await _context.Citas
                .AsNoTracking()
                .Where(c => c.IdCita == id)
                .Select(c => ToDTO(c))
                .FirstOrDefaultAsync();
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
            c.Motivo = actualizado.Motivo;
            c.Notas = actualizado.Notas;
            c.Diagnostico = actualizado.Diagnostico;
            c.Tratamiento = actualizado.Tratamiento;
            c.Estado = actualizado.Estado;
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

        private static CitaResponseDTO ToDTO(Cita c) => new()
        {
            Id = c.IdCita,
            Codigo = c.Codigo,
            FechaHora = c.FechaHora,
            Motivo = c.Motivo,
            Notas = c.Notas,
            Diagnostico = c.Diagnostico,
            Tratamiento = c.Tratamiento,
            Estado = c.Estado,
            PacienteId = c.IdPaciente,
            VeterinarioId = c.IdVeterinario,
            ServicioId = c.IdServicio
        };
    }
}
