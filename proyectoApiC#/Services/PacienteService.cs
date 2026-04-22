using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyectoApiC_.Data;
using proyectoApiC_.Models;

namespace proyectoApiC_.Services
{
    public class PacienteService
    {
        private readonly AppDbContext _context;

        public PacienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Paciente>> ObtenerTodos()
        {
            return await _context.Pacientes.Include(p => p.Cliente).ToListAsync();
        }

        public async Task<List<Paciente>> BusquedaGlobal(string? termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
            {
                return await _context.Pacientes.Include(p => p.Cliente).ToListAsync();
            }

            string t = termino.ToLower();
            
            return await _context.Pacientes
                .Include(p => p.Cliente)
                .Where(p => 
                    p.Codigo.ToLower().Contains(t) ||
                    p.Nombre.ToLower().Contains(t) ||
                    (p.Cliente != null && p.Cliente.Nombre.ToLower().Contains(t)) ||
                    (p.Cliente != null && p.Cliente.Apellido.ToLower().Contains(t)))
                .ToListAsync();
        }

        public async Task<Paciente?> ObtenerPorId(long id)
        {
            return await _context.Pacientes.Include(p => p.Cliente).FirstOrDefaultAsync(p => p.IdPaciente == id);
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

            paciente.Nombre = actualizado.Nombre ?? paciente.Nombre;
            paciente.Especie = actualizado.Especie ?? paciente.Especie;
            paciente.Raza = actualizado.Raza ?? paciente.Raza;
            paciente.Edad = actualizado.Edad ?? paciente.Edad;
            paciente.Peso = actualizado.Peso ?? paciente.Peso;
            paciente.Color = actualizado.Color ?? paciente.Color;
            paciente.Alergias = actualizado.Alergias ?? paciente.Alergias;
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
    }
}
