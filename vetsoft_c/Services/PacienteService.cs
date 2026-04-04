using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vetsoft_c.DTOs;
using vetsoft_c.Models;
using vetsoft_c.Repositories;

namespace vetsoft_c.Services
{
    public class PacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
            => _pacienteRepository = pacienteRepository;

        public async Task<List<PacienteResponseDTO>> ObtenerTodos()
        {
            var pacientes = await _pacienteRepository.GetAllAsync();
            return pacientes.Select(p => ToDTO(p)).ToList();
        }

        public async Task<List<PacienteResponseDTO>> BusquedaGlobal(string? termino)
        {
            var pacientes = await _pacienteRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(termino))
            {
                string t = termino.ToLower();
                pacientes = pacientes.Where(p =>
                    p.Codigo.ToLower().Contains(t) ||
                    p.Nombre.ToLower().Contains(t) ||
                    p.Cliente!.Nombre.ToLower().Contains(t) ||
                    p.Cliente!.Apellido.ToLower().Contains(t)).ToList();
            }

            return pacientes.Select(p => ToDTO(p)).ToList();
        }

        public async Task<PacienteResponseDTO?> ObtenerPorId(long id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync((int)id);
            return paciente != null ? ToDTO(paciente) : null;
        }

        public async Task<Paciente> Crear(Paciente paciente)
        {
            var creado = await _pacienteRepository.AddAsync(paciente);
            await _pacienteRepository.SaveChangesAsync();
            return creado;
        }

        public async Task<Paciente> Actualizar(long id, Paciente actualizado)
        {
            var paciente = await _pacienteRepository.GetByIdAsync((int)id);
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

            await _pacienteRepository.UpdateAsync(paciente);
            await _pacienteRepository.SaveChangesAsync();
            return paciente;
        }

        public async Task Eliminar(long id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync((int)id);
            if (paciente != null)
            {
                paciente.Estado = false;
                await _pacienteRepository.UpdateAsync(paciente);
                await _pacienteRepository.SaveChangesAsync();
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
