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
    public class PacienteService
    {
        private readonly AppDbContext _context;
        public PacienteService(AppDbContext context) => _context = context;

        public async Task<List<PacienteResponseDTO>> ObtenerTodos()
        {
            return await _context.Pacientes
                .AsNoTracking()
                .Select(p => ToDTO(p))
                .ToListAsync();
        }

        public async Task<List<PacienteResponseDTO>> BusquedaGlobal(string? termino)
        {
            var query = _context.Pacientes.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(termino))
            {
                string t = termino.ToLower();
                query = query.Where(p =>
                    p.Codigo.ToLower().Contains(t) ||
                    p.Nombre.ToLower().Contains(t) ||
                    p.Cliente!.Nombre.ToLower().Contains(t) ||
                    p.Cliente!.Apellido.ToLower().Contains(t));
            }

            return await query.Select(p => ToDTO(p)).ToListAsync();
        }

        public async Task<PacienteResponseDTO?> ObtenerPorId(long id)
        { 
            return await _context.Pacientes
                .AsNoTracking()
                .Where(p => p.IdPaciente == id)
                .Select(p => ToDTO(p))
                .FirstOrDefaultAsync();
        }

        public async Task<Paciente> Crear(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        public async Task<Paciente> Actualizar(long id, Paciente actualizado)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null) throw new Exception("No encontrado");

            paciente.Nombre = actualizado.Nombre;
            paciente.Especie = actualizado.Especie;
            paciente.Raza = actualizado.Raza;
            paciente.Edad = actualizado.Edad;
            paciente.Peso = actualizado.Peso;
            paciente.Color = actualizado.Color;
            paciente.Alergias = actualizado.Alergias;
            paciente.IdCliente = actualizado.IdCliente;
            paciente.Estado = actualizado.Estado;

            await _context.SaveChangesAsync();
            return paciente;
        }

        public async Task Eliminar(long id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                paciente.Estado = false;
                await _context.SaveChangesAsync();
            }
        }

        private static PacienteResponseDTO ToDTO(Paciente p) => new()
        {
            Id = p.IdPaciente,
            Codigo = p.Codigo,
            Nombre = p.Nombre,
            Especie = p.Especie,
            Raza = p.Raza,
            Edad = p.Edad,
            Peso = p.Peso,
            Color = p.Color,
            Alergias = p.Alergias,
            Estado = p.Estado,
            IdCliente = p.IdCliente
        };
        
    }
}
