using Microsoft.EntityFrameworkCore;
using vetsoft_c.Data;
using vetsoft_c.Models;

namespace vetsoft_c.Repositories
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

        public async Task<IEnumerable<Servicio>> GetByCodigoAsync(string codigo)
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.Codigo.Contains(codigo))
                .ToListAsync();
        }

        public async Task<IEnumerable<Servicio>> GetByRangoPrecionAsync(double precioMin, double precioMax)
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.Precio >= precioMin && s.Precio <= precioMax)
                .ToListAsync();
        }

        public async Task<IEnumerable<Servicio>> GetByEstadoAsync(bool estado)
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.Estado == estado)
                .ToListAsync();
        }
    }
}
