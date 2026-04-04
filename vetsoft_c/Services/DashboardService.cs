using System;
using System.Threading.Tasks;
using vetsoft_c.DTOs;
using vetsoft_c.Repositories;

namespace vetsoft_c.Services
{
    public class DashboardService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IVeterinarioRepository _veterinarioRepository;
        private readonly IServicioRepository _servicioRepository;
        private readonly ICitaRepository _citaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public DashboardService(
            IClienteRepository clienteRepository,
            IPacienteRepository pacienteRepository,
            IVeterinarioRepository veterinarioRepository,
            IServicioRepository servicioRepository,
            ICitaRepository citaRepository,
            IUsuarioRepository usuarioRepository)
        {
            _clienteRepository = clienteRepository;
            _pacienteRepository = pacienteRepository;
            _veterinarioRepository = veterinarioRepository;
            _servicioRepository = servicioRepository;
            _citaRepository = citaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<DashboardStatsDTO> ObtenerEstadisticasAsync()
        {
            var stats = new DashboardStatsDTO
            {
                TotalClientes = await _clienteRepository.CountAsync(c => c.Estado),
                TotalPacientes = await _pacienteRepository.CountAsync(p => p.Estado),
                TotalVeterinarios = await _veterinarioRepository.CountAsync(v => v.Estado),
                TotalServicios = await _servicioRepository.CountAsync(s => s.Estado),
                TotalCitas = await _citaRepository.CountAsync(c => true),
                TotalUsuarios = await _usuarioRepository.CountAsync(u => u.Estado),
                CitasHoy = await _citaRepository.CountAsync(c => c.FechaHora.Date == DateTime.Today),
                CitasPendientes = await _citaRepository.CountAsync(c => c.Estado == "PENDIENTE")
            };

            return stats;
        }
    }
}
