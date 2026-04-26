using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetsoft_c.Data;
using vetsoft_c.DTOs;

namespace vetsoft_c.Services
{
    public class DashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatsDTO> ObtenerEstadisticasAsync()
        {
            var stats = new DashboardStatsDTO
            {
                TotalClientes = await _context.Clientes.CountAsync(c => c.Estado),
                TotalPacientes = await _context.Pacientes.CountAsync(p => p.Estado),
                TotalVeterinarios = await _context.Veterinarios.CountAsync(v => v.Estado),
                TotalServicios = await _context.Servicios.CountAsync(s => s.Estado),
                TotalCitas = await _context.Citas.CountAsync(),
                TotalUsuarios = await _context.Usuarios.CountAsync(u => u.Estado),
                CitasHoy = await _context.Citas.CountAsync(c => c.FechaHora.Date == DateTime.Today),
                CitasPendientes = await _context.Citas.CountAsync(c => c.Estado == "PENDIENTE")
            };

            return stats;
        }
    }
}
