using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyectoApiC_.Data;
using proyectoApiC_.Models;

namespace proyectoApiC_.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ObtenerTodos()
        {
            return await _context.Usuarios.Where(u => u.Estado).ToListAsync();
        }

        public async Task<List<Usuario>> BusquedaGlobal(string? termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
            {
                return await _context.Usuarios.Where(u => u.Estado).ToListAsync();
            }

            string t = termino.ToLower();
            
            return await _context.Usuarios
                .Where(u => u.Estado && (
                    u.Codigo.ToLower().Contains(t) ||
                    u.Nombre.ToLower().Contains(t) ||
                    u.Apellido.ToLower().Contains(t) ||
                    u.Correo.ToLower().Contains(t)))
                .ToListAsync();
        }

        public async Task<Usuario?> ObtenerPorId(long id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id && u.Estado);
        }

        public async Task<Usuario?> ObtenerPorCorreo(string correo)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo && u.Estado);
        }

        public async Task<Usuario?> ObtenerPorCodigo(string codigo)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Codigo == codigo && u.Estado);
        }

        public async Task<bool> CorreoExiste(string correo)
        {
            return await _context.Usuarios.AnyAsync(u => u.Correo == correo);
        }

        public async Task<bool> CodigoExiste(string codigo)
        {
            return await _context.Usuarios.AnyAsync(u => u.Codigo == codigo);
        }

        public async Task<Usuario> Crear(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre)) throw new ArgumentException("El nombre es requerido");
            if (string.IsNullOrWhiteSpace(usuario.Apellido)) throw new ArgumentException("El apellido es requerido");
            if (string.IsNullOrWhiteSpace(usuario.Correo)) throw new ArgumentException("El correo es requerido");
            if (string.IsNullOrWhiteSpace(usuario.Contrasena)) throw new ArgumentException("La contraseña es requerida");
            
            if (await CorreoExiste(usuario.Correo))
                throw new ArgumentException("El correo ya está registrado");
            
            if (await CodigoExiste(usuario.Codigo))
                throw new ArgumentException("El código ya existe");

            usuario.Estado = true;
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> Actualizar(long id, Usuario actualizado)
        {
            var u = await _context.Usuarios.FindAsync(id);
            if (u == null) throw new Exception("Usuario no encontrado");

            if (!string.IsNullOrWhiteSpace(actualizado.Nombre))
                u.Nombre = actualizado.Nombre;
            
            if (!string.IsNullOrWhiteSpace(actualizado.Apellido))
                u.Apellido = actualizado.Apellido;
            
            if (!string.IsNullOrWhiteSpace(actualizado.Correo))
                u.Correo = actualizado.Correo;
            
            if (!string.IsNullOrWhiteSpace(actualizado.Contrasena))
                u.Contrasena = actualizado.Contrasena;

            u.Rol = actualizado.Rol;
            u.Estado = actualizado.Estado;

            await _context.SaveChangesAsync();
            return u;
        }

        public async Task Eliminar(long id)
        {
            var u = await _context.Usuarios.FindAsync(id);
            if (u != null)
            {
                u.Estado = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<long> ContarActivos()
        {
            return await _context.Usuarios.CountAsync(u => u.Estado);
        }

        public async Task<List<Usuario>> ObtenerPorRol(string rol)
        {
            return await _context.Usuarios
                .Where(u => u.Estado && u.RolString == rol)
                .ToListAsync();
        }
    }
}
