using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vetsoft_c.Models;
using vetsoft_c.Repositories;

namespace vetsoft_c.Services
{
    public class VeterinarioService
    {
        private readonly IVeterinarioRepository _veterinarioRepository;

        public VeterinarioService(IVeterinarioRepository veterinarioRepository)
        {
            _veterinarioRepository = veterinarioRepository;
        }

        public async Task<List<Veterinario>> ObtenerTodos()
        {
            var veterinarios = await _veterinarioRepository.GetAllAsync();
            return veterinarios.ToList();
        }

        public async Task<List<Veterinario>> BusquedaGlobal(string? termino)
        {
            var veterinarios = await _veterinarioRepository.GetAllAsync();

            if (string.IsNullOrWhiteSpace(termino))
            {
                return veterinarios.ToList();
            }

            string t = termino.ToLower();

            return veterinarios
                .Where(v =>
                    v.Codigo.ToLower().Contains(t) ||
                    v.Nombre.ToLower().Contains(t) ||
                    v.Apellido.ToLower().Contains(t) ||
                    (v.NumeroColegiado != null && v.NumeroColegiado.ToLower().Contains(t)))
                .ToList();
        }

        public async Task<Veterinario?> ObtenerPorId(long id)
        {
            return await _veterinarioRepository.GetByIdAsync((int)id);
        }

        public async Task<Veterinario> Crear(Veterinario veterinario)
        {
            var creado = await _veterinarioRepository.AddAsync(veterinario);
            await _veterinarioRepository.SaveChangesAsync();
            return creado;
        }

        public async Task<Veterinario> Actualizar(long id, Veterinario actualizado)
        {
            var v = await _veterinarioRepository.GetByIdAsync((int)id);
            if (v == null) throw new Exception("No encontrado");

            v.Nombre = actualizado.Nombre ?? v.Nombre;
            v.Apellido = actualizado.Apellido ?? v.Apellido;
            v.NumeroColegiado = actualizado.NumeroColegiado ?? v.NumeroColegiado;
            v.Especialidad = actualizado.Especialidad ?? v.Especialidad;
            v.Correo = actualizado.Correo ?? v.Correo;
            v.Telefono = actualizado.Telefono ?? v.Telefono;
            v.Estado = actualizado.Estado;

            await _veterinarioRepository.UpdateAsync(v);
            await _veterinarioRepository.SaveChangesAsync();
            return v;
        }

        public async Task Eliminar(long id)
        {
            var v = await _veterinarioRepository.GetByIdAsync((int)id);
            if (v != null)
            {
                v.Estado = false;
                await _veterinarioRepository.UpdateAsync(v);
                await _veterinarioRepository.SaveChangesAsync();
            }
        }
    }
}
