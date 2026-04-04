using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vetsoft_c.DTOs;
using vetsoft_c.Models;
using vetsoft_c.Repositories;

namespace vetsoft_c.Services
{
    public class CitaService
    {
        private readonly ICitaRepository _citaRepository;

        public CitaService(ICitaRepository citaRepository)
            => _citaRepository = citaRepository;

        public async Task<List<CitaResponseDTO>> ObtenerTodos()
        {
            var citas = await _citaRepository.GetAllAsync();
            return citas.Select(c => ToDTO(c)).ToList();
        }

        public async Task<List<CitaResponseDTO>> BusquedaGlobal(string? termino)
        {
            var citas = await _citaRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(termino))
            {
                string t = termino.ToLower();
                citas = citas.Where(c =>
                    c.Codigo.ToLower().Contains(t) ||
                    c.Paciente!.Nombre.ToLower().Contains(t) ||
                    c.Veterinario!.Nombre.ToLower().Contains(t) ||
                    c.Veterinario!.Apellido.ToLower().Contains(t) ||
                    c.Servicio!.Nombre.ToLower().Contains(t)).ToList();
            }

            return citas.Select(c => ToDTO(c)).ToList();
        }

        public async Task<CitaResponseDTO?> ObtenerPorId(long id)
        {
            var cita = await _citaRepository.GetByIdAsync((int)id);
            return cita != null ? ToDTO(cita) : null;
        }

        public async Task<Cita> Crear(Cita cita)
        {
            var creada = await _citaRepository.AddAsync(cita);
            await _citaRepository.SaveChangesAsync();
            return creada;
        }

        public async Task<Cita> Actualizar(long id, Cita actualizado)
        {
            var c = await _citaRepository.GetByIdAsync((int)id);
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

            await _citaRepository.UpdateAsync(c);
            await _citaRepository.SaveChangesAsync();
            return c;
        }

        public async Task Eliminar(long id)
        {
            var c = await _citaRepository.GetByIdAsync((int)id);
            if (c != null)
            {
                c.Estado = "CANCELADA";
                await _citaRepository.UpdateAsync(c);
                await _citaRepository.SaveChangesAsync();
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
            IdPaciente = c.IdPaciente,
            IdVeterinario = c.IdVeterinario,
            IdServicio = c.IdServicio
        };
    }
}
