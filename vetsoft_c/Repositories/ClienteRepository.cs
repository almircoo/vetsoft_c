using Microsoft.EntityFrameworkCore;
using vetsoft_c.Data;
using vetsoft_c.Models;

namespace vetsoft_c.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Cliente?> GetByCorreoAsync(string correo)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Correo == correo);
        }

        public async Task<Cliente?> GetByTelefonoAsync(string telefono)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Telefono == telefono);
        }

        public async Task<IEnumerable<Cliente>> GetByNombreAsync(string nombre)
        {
            return await _dbSet.AsNoTracking()
                .Where(c => c.Nombre.Contains(nombre))
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> GetByCodigoAsync(string codigo)
        {
            return await _dbSet.AsNoTracking()
                .Where(c => c.Codigo.Contains(codigo))
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> GetByEstadoAsync(bool estado)
        {
            return await _dbSet.AsNoTracking()
                .Where(c => c.Estado == estado)
                .ToListAsync();
        }
    }
}
