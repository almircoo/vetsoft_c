using Microsoft.AspNetCore.Mvc;
using vetsoft_c.DTOs;
using vetsoft_c.Services;

namespace vetsoft_c.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var usuario = await _authService.LoginAsync(loginDto);

                if (usuario == null)
                {
                    return Unauthorized(new { message = "Correo o contraseña incorrectos" });
                }

                return Ok(new { message = "Login exitoso", user = usuario });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioCreateDTO registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var usuario = await _authService.RegisterAsync(registerDto);

                return CreatedAtAction(nameof(ObtenerUsuario), new { id = usuario.IdUsuario }, new
                {
                    message = "Registro exitoso",
                    user = usuario
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al registrar usuario", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuario(long id)
        {
            try
            {
                var usuario = await _authService.ObtenerUsuarioPorId(id);

                if (usuario == null)
                    return NotFound(new { message = "Usuario no encontrado" });

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener usuario", error = ex.Message });
            }
        }
    }
}
