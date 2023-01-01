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
        Task<UsuarioResponseDTO?> GetUserByIdAsync(long id);
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
            var usuario = await _usuarioRepository.GetByCorreoAsync(loginDto.Correo);

            if (usuario == null)
                return null;

            if (!BCrypt.Net.BCrypt.EnhancedVerify(loginDto.Contrasena, usuario.Contrasena))
                return null;

            return MapToResponseDTO(usuario);
        }

        public async Task<UsuarioResponseDTO?> RegisterAsync(UsuarioCreateDTO registerDto)
        {
            var existingEmail = await _usuarioRepository.GetByCorreoAsync(registerDto.Correo);
            if (existingEmail != null)
                return null;

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

            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();

            return MapToResponseDTO(usuario);
        }

        public async Task<UsuarioResponseDTO?> GetUserByIdAsync(long id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return usuario == null ? null : MapToResponseDTO(usuario);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, hashType: HashType.SHA512);
        }

        private UsuarioResponseDTO MapToResponseDTO(Usuario usuario)
        {
            return new UsuarioResponseDTO
            {
                Id = usuario.IdUsuario,
                Codigo = usuario.Codigo,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                Rol = usuario.Rol.ToString(),
                Estado = usuario.Estado
            };
        }
    }
}