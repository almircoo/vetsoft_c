using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vetsoft_c.Models;
using vetsoft_c.Repositories;

namespace vetsoft_c.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Usuario>> ObtenerTodos()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.ToList();
        }

        public async Task<List<Usuario>> BusquedaGlobal(string? termino)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            if (string.IsNullOrWhiteSpace(termino))
            {
                return usuarios.ToList();
            }

            string t = termino.ToLower();

            return usuarios
                .Where(u =>
                    u.Codigo.ToLower().Contains(t) ||
                    u.Nombre.ToLower().Contains(t) ||
                    u.Apellido.ToLower().Contains(t) ||
                    u.Correo.ToLower().Contains(t))
                .ToList();
        }

        public async Task<Usuario?> ObtenerPorId(long id)
        {
            return await _usuarioRepository.GetByIdAsync((int)id);
        }

        public async Task<Usuario> Crear(Usuario usuario)
        {
            var creado = await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();
            return creado;
        }

        public async Task<Usuario> Actualizar(long id, Usuario actualizado)
        {
            var u = await _usuarioRepository.GetByIdAsync((int)id);
            if (u == null) throw new Exception("No encontrado");

            u.Nombre = actualizado.Nombre ?? u.Nombre;
            u.Apellido = actualizado.Apellido ?? u.Apellido;
            u.Correo = actualizado.Correo ?? u.Correo;
            if (!string.IsNullOrEmpty(actualizado.Contrasena))
            {
                u.Contrasena = actualizado.Contrasena;
            }
            u.Rol = actualizado.Rol;
            u.Estado = actualizado.Estado;

            await _usuarioRepository.UpdateAsync(u);
            await _usuarioRepository.SaveChangesAsync();
            return u;
        }

        public async Task Eliminar(long id)
        {
            var u = await _usuarioRepository.GetByIdAsync((int)id);
            if (u != null)
            {
                u.Estado = false;
                await _usuarioRepository.UpdateAsync(u);
                await _usuarioRepository.SaveChangesAsync();
            }
        }
    }
}
