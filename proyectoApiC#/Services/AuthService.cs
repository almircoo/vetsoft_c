using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyectoApiC_.Data;
using proyectoApiC_.Models;
using proyectoApiC_.DTOs;

namespace proyectoApiC_.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioResponseDTO?> LoginAsync(UsuarioLoginDTO loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Correo) || string.IsNullOrWhiteSpace(loginDto.Contrasena))
                throw new ArgumentException("El correo y contraseña son requeridos");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == loginDto.Correo && u.Estado);
            
            if (usuario == null)
                return null;

            if (!VerifyPassword(loginDto.Contrasena, usuario.Contrasena))
                return null;

            return MapToResponseDTO(usuario);
        }

        public async Task<UsuarioResponseDTO?> RegisterAsync(UsuarioCreateDTO registerDto)
        {
            if (string.IsNullOrWhiteSpace(registerDto.Nombre)) 
                throw new ArgumentException("El nombre es requerido");
            if (string.IsNullOrWhiteSpace(registerDto.Apellido)) 
                throw new ArgumentException("El apellido es requerido");
            if (string.IsNullOrWhiteSpace(registerDto.Correo)) 
                throw new ArgumentException("El correo es requerido");
            if (string.IsNullOrWhiteSpace(registerDto.Contrasena)) 
                throw new ArgumentException("La contraseña es requerida");

            var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == registerDto.Correo);
            if (existingUser != null)
                throw new ArgumentException("El correo ya está registrado");

            var usuario = new Usuario
            {
                Codigo = registerDto.Codigo,
                Nombre = registerDto.Nombre,
                Apellido = registerDto.Apellido,
                Correo = registerDto.Correo,
                Contrasena = HashPassword(registerDto.Contrasena),
                RolString = registerDto.Rol,
                Estado = true
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return MapToResponseDTO(usuario);
        }

        public async Task<UsuarioResponseDTO?> ObtenerUsuarioPorId(long id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id && u.Estado);
            return usuario == null ? null : MapToResponseDTO(usuario);
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hash;
        }

        private UsuarioResponseDTO MapToResponseDTO(Usuario usuario)
        {
            return new UsuarioResponseDTO
            {
                IdUsuario = usuario.IdUsuario,
                Codigo = usuario.Codigo,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                Rol = usuario.RolString,
                Estado = usuario.Estado
            };
        }
    }
}
