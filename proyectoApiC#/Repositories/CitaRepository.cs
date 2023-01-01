using Microsoft.EntityFrameworkCore;
using proyectoApiC_.Data;
using proyectoApiC_.Models;

namespace proyectoApiC_.Repositories
{
    public class CitaRepository : Repository<Cita>, ICitaRepository
    {
        public CitaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cita>> GetByVeterinarioIdAsync(long veterinarioId)
        {
            return await _dbSet.AsNoTracking()
                .Where(c => c.IdVeterinario == veterinarioId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetByPacienteIdAsync(long pacienteId)
        {
            return await _dbSet.AsNoTracking()
                .Where(c => c.IdPaciente == pacienteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetByFechaAsync(DateTime fecha)
        {
            return await _dbSet.AsNoTracking()
                .Where(c => c.FechaHora.Date == fecha.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetByEstadoAsync(string estado)
        {
            return await _dbSet.AsNoTracking()
                .Where(c => c.Estado == estado)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetByServicioIdAsync(long servicioId)
        {
            return await _dbSet.AsNoTracking()
                .Where(c => c.IdServicio == servicioId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetByCodigoAsync(string codigo)
        {
            return await _dbSet.AsNoTracking()
                .Where(c => c.Codigo.Contains(codigo))
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _dbSet.AsNoTracking()
                .Where(c => c.FechaHora.Date >= fechaInicio.Date && c.FechaHora.Date <= fechaFin.Date)
                .ToListAsync();
        }
    }
}
