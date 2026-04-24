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
                    _logger.LogWarning($"Fallo el login para el usuaio: {loginDto.NombreUsuario}");
                    return Unauthorized(new { message = "Invalid credentials" });
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Rol)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24) // Cookie expires in 24 hours
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                _logger.LogInformation($"Usuario logeado correctamente: {usuario.NombreUsuario}");

                return Ok(new
                {
                    message = "Login successful",
                    user = usuario
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Login error: {ex.Message}");
                return StatusCode(500, new { message = "Ocurrio un error en el login" });
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
                    _logger.LogWarning($"Fallo el registro intentente nuevamente: {registerDto.NombreUsuario}");
                    return BadRequest(new { message = "Usuario ya existe inicie session" });
                }

                _logger.LogInformation($"Usuario registrado correctamente: {usuario.NombreUsuario}");

                return CreatedAtAction(nameof(GetCurrentUser), new { id = usuario.Id }, new
                {
                    message = "Registro successful",
                    user = usuario
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Registro error: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred during registration" });
            }
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return Unauthorized();

                if (!int.TryParse(userIdClaim.Value, out int userId))
                    return Unauthorized();

                var usuario = await _authService.GetUserByIdAsync(userId);

                if (usuario == null)
                    return NotFound();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al traer  al usuario: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred" });
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "Desconocido";

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                _logger.LogInformation($"Usuario desconectado: {userName}");

                return Ok(new { message = "Logout successful" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Logout error: {ex.Message}");
                return StatusCode(500, new { message = "surgio un error en el logout" });
            }
        }
    }
}
