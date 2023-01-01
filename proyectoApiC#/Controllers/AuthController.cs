using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using proyectoApiC_.DTOs;
using proyectoApiC_.Services;
using System.Security.Claims;

namespace proyectoApiC_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var usuario = await _authService.LoginAsync(loginDto);

                if (usuario == null)
                {
                    _logger.LogWarning($"Intento de login fallido para el correo: {loginDto.Correo}");
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                    new Claim(ClaimTypes.Email, usuario.Correo),
                    new Claim(ClaimTypes.Role, usuario.Rol)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                _logger.LogInformation($"Usuario logueado correctamente: {usuario.Correo}");

                return Ok(new { message = "Login exitoso", user = usuario });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error crítico durante el login");
                return StatusCode(500, new { message = "Ocurrió un error interno en el servidor" });
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UsuarioCreateDTO registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var usuario = await _authService.RegisterAsync(registerDto);

                if (usuario == null)
                {
                    return BadRequest(new { message = "No se pudo registrar el usuario, verifique los datos" });
                }

                _logger.LogInformation($"Usuario registrado: {usuario.Correo}");

                return CreatedAtAction(nameof(GetCurrentUser), new { id = usuario.Id }, new
                {
                    message = "Registro exitoso",
                    user = usuario
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el registro");
                return StatusCode(500, new { message = "Ocurrió un error al registrar el usuario" });
            }
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null) return Unauthorized();

                if (!long.TryParse(userIdClaim.Value, out long userId))
                    return Unauthorized();

                var usuario = await _authService.GetUserByIdAsync((int)userId);

                if (usuario == null) return NotFound();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar perfil de usuario");
                return StatusCode(500, new { message = "Error al obtener usuario" });
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value ?? "Desconocido";
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                _logger.LogInformation($"Usuario desconectado: {email}");
                return Ok(new { message = "Logout exitoso" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el logout");
                return StatusCode(500, new { message = "Error al cerrar sesión" });
            }
        }
    }
}