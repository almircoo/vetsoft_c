using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetsoft_c.Data;
using vetsoft_c.Models;

namespace vetsoft_c.Services
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
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<List<Usuario>> BusquedaGlobal(string? termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
            {
                return await _context.Usuarios.ToListAsync();
            }

            string t = termino.ToLower();
            
            return await _context.Usuarios
                .Where(u => 
                    u.Codigo.ToLower().Contains(t) ||
                    u.Nombre.ToLower().Contains(t) ||
                    u.Apellido.ToLower().Contains(t) ||
                    u.Correo.ToLower().Contains(t))
                .ToListAsync();
        }

        public async Task<Usuario?> ObtenerPorId(long id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> Crear(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> Actualizar(long id, Usuario actualizado)
        {
            var u = await _context.Usuarios.FindAsync(id);
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
    }
}
