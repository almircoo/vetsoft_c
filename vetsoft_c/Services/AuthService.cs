using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using vetsoft_c.Models;
using vetsoft_c.DTOs;
using vetsoft_c.Repositories;

namespace vetsoft_c.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioResponseDTO?> LoginAsync(UsuarioLoginDTO loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Correo) || string.IsNullOrWhiteSpace(loginDto.Contrasena))
                throw new ArgumentException("El correo y contraseña son requeridos");

            var usuario = await _usuarioRepository.GetByCorreoAsync(loginDto.Correo);

            if (usuario == null || !usuario.Estado)
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

            var existingUser = await _usuarioRepository.GetByCorreoAsync(registerDto.Correo);
            if (existingUser != null)
                throw new ArgumentException("El correo ya está registrado");

            var usuario = new Usuario
            {
                Nombre = registerDto.Nombre,
                Apellido = registerDto.Apellido,
                Correo = registerDto.Correo,
                Contrasena = HashPassword(registerDto.Contrasena),
                RolString = registerDto.Rol,
                Estado = true
            };

            var creado = await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();

            return MapToResponseDTO(creado);
        }

        public async Task<UsuarioResponseDTO?> ObtenerUsuarioPorId(long id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync((int)id);
            return usuario != null && usuario.Estado ? MapToResponseDTO(usuario) : null;
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
