using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vetsoft_c.Models;
using vetsoft_c.Repositories;

namespace vetsoft_c.Services
{
    public class ServicioService
    {
        private readonly IServicioRepository _servicioRepository;

        public ServicioService(IServicioRepository servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        public async Task<List<Servicio>> ObtenerTodos()
        {
            var servicios = await _servicioRepository.GetAllAsync();
            return servicios.ToList();
        }

        public async Task<List<Servicio>> BusquedaGlobal(string? termino)
        {
            var servicios = await _servicioRepository.GetAllAsync();

            if (string.IsNullOrWhiteSpace(termino))
            {
                return servicios.ToList();
            }

            string t = termino.ToLower();

            return servicios
                .Where(s =>
                    s.Codigo.ToLower().Contains(t) ||
                    s.Nombre.ToLower().Contains(t) ||
                    (s.Descripcion != null && s.Descripcion.ToLower().Contains(t)) ||
                    (s.DuracionEstimada != null && s.DuracionEstimada.ToString()!.Contains(t)) ||
                    s.Precio.ToString().Contains(t) ||
                    (t == "activo" && s.Estado == true) ||
                    (t == "inactivo" && s.Estado == false))
                .ToList();
        }

        public async Task<List<Servicio>> ObtenerActivos()
        {
            var activos = await _servicioRepository.GetByEstadoAsync(true);
            return activos.OrderBy(s => s.Nombre).ToList();
        }

        public async Task<Servicio?> ObtenerPorId(long id)
        {
            return await _servicioRepository.GetByIdAsync((int)id);
        }

        //public async Task<List<Servicio>> BuscarPorNombre(string nombre)
        //{
        //    var resultados = await _servicioRepository.GetByNombreAsync(nombre);
        //    return resultados;
        //}

        public async Task<List<Servicio>> BuscarPorRangoPrecio(double min, double max)
        {
            var servicios = await _servicioRepository.GetAllAsync();
            return servicios
                .Where(s => s.Precio >= min && s.Precio <= max && s.Estado == true)
                .ToList();
        }

        public async Task<Servicio> Crear(Servicio servicio)
        {
            var creado = await _servicioRepository.AddAsync(servicio);
            await _servicioRepository.SaveChangesAsync();
            return creado;
        }

        public async Task<Servicio> Actualizar(long id, Servicio actualizado)
        {
            var servicio = await _servicioRepository.GetByIdAsync((int)id);
            if (servicio == null) throw new Exception("No encontrado");

            servicio.Nombre = actualizado.Nombre ?? servicio.Nombre;
            servicio.Descripcion = actualizado.Descripcion ?? servicio.Descripcion;
            servicio.Precio = actualizado.Precio != 0 ? actualizado.Precio : servicio.Precio;
            servicio.DuracionEstimada = actualizado.DuracionEstimada ?? servicio.DuracionEstimada;
            servicio.Estado = actualizado.Estado;

            await _servicioRepository.UpdateAsync(servicio);
            await _servicioRepository.SaveChangesAsync();
            return servicio;
        }

        public async Task Eliminar(long id)
        {
            var servicio = await _servicioRepository.GetByIdAsync((int)id);
            if (servicio != null)
            {
                servicio.Estado = false;
                await _servicioRepository.UpdateAsync(servicio);
                await _servicioRepository.SaveChangesAsync();
            }
        }

        public async Task<long> ContarActivos()
        {
            return await _servicioRepository.CountAsync(s => s.Estado == true);
        }
    }
}
