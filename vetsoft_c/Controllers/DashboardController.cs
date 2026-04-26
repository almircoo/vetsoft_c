using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vetsoft_c.DTOs;
using vetsoft_c.Services;

namespace vetsoft_c.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("stats")]
        public async Task<ActionResult<DashboardStatsDTO>> ObtenerEstadisticas()
        {
            try
            {
                var stats = await _dashboardService.ObtenerEstadisticasAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener estadísticas", error = ex.Message });
            }
        }
    }
}
