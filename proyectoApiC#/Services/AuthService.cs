using proyectoApiC_.Models;
using proyectoApiC_.Repositories;
using proyectoApiC_.DTOs;
using BCrypt.Net;

namespace proyectoApiC_.Services
{

    public interface IAuthService
    {
        Task<UsuarioResponseDTO?> LoginAsync(UsuarioLoginDTO loginDto);
        Task<UsuarioResponseDTO?> RegisterAsync(UsuarioCreateDTO registerDto);
        Task<UsuarioResponseDTO?> GetUserByIdAsync(int id);
        Task<bool> ValidatePasswordAsync(string password, string hashedPassword);
        string HashPassword(string password);
    }

    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioResponseDTO?> LoginAsync(UsuarioLoginDTO loginDto)
        {
            var usuario = await _usuarioRepository.GetByNombreUsuarioAsync(loginDto.NombreUsuario);

            if (usuario == null)
                return null;

            // Validate password
            if (!BCrypt.Net.BCrypt.EnhancedVerify(loginDto.Contraseña, usuario.Contraseña))
                return null;

            return MapToResponseDTO(usuario);
        }

        public async Task<UsuarioResponseDTO?> RegisterAsync(UsuarioCreateDTO registerDto)
        {
            var existingUser = await _usuarioRepository.GetByNombreUsuarioAsync(registerDto.NombreUsuario);
            if (existingUser != null)
                return null;

            var existingEmail = await _usuarioRepository.GetByEmailAsync(registerDto.Email);
            if (existingEmail != null)
                return null;

            var hashedPassword = HashPassword(registerDto.Contraseña);

            var usuario = new Usuario
            {
                NombreUsuario = registerDto.NombreUsuario,
                Email = registerDto.Email,
                Contraseña = hashedPassword,
                Rol = registerDto.Rol,
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow
            };

            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();

            return MapToResponseDTO(usuario);
        }

        public async Task<UsuarioResponseDTO?> GetUserByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                return null;

            return MapToResponseDTO(usuario);
        }

        public async Task<bool> ValidatePasswordAsync(string password, string hashedPassword)
        {
            return await Task.FromResult(BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword));
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, hashType: HashType.SHA512);
        }

        private UsuarioResponseDTO MapToResponseDTO(Usuario usuario)
        {
            return new UsuarioResponseDTO
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Email = usuario.Email,
                Rol = usuario.Rol,
                FechaCreacion = usuario.FechaCreacion,
                FechaActualizacion = usuario.FechaActualizacion
            };
        }
    }
}
