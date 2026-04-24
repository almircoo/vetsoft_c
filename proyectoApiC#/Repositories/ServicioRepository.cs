using Microsoft.EntityFrameworkCore;
using proyectoApiC_.Data;
using proyectoApiC_.Models;

namespace proyectoApiC_.Repositories
{
    public class ServicioRepository : Repository<Servicio>, IServicioRepository
    {
        public ServicioRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Servicio?> GetByNombreAsync(string nombre)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(s => s.Nombre == nombre);
        }

        public async Task<IEnumerable<Servicio>> GetByTipoAsync(string tipo)
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.Tipo == tipo)
                .ToListAsync();
        }
    }
}
