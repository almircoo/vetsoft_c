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

        public async Task<IEnumerable<Cita>> GetByVeterinarioIdAsync(int veterinarioId)
        {
            return await _dbSet.AsNoTracking()
                .Where(c => c.VeterinarioId == veterinarioId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetByPacienteIdAsync(int pacienteId)
        {
            return await _dbSet.AsNoTracking()
                .Where(c => c.PacienteId == pacienteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetByFechaAsync(DateTime fecha)
        {
            return await _dbSet.AsNoTracking()
                .Where(c => c.Fecha.Date == fecha.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetByEstadoAsync(string estado)
        {
            return await _dbSet.AsNoTracking()
                .Where(c => c.Estado == estado)
                .ToListAsync();
        }
    }
}
