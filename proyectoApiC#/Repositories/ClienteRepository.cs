using Microsoft.EntityFrameworkCore;
using proyectoApiC_.Data;
using proyectoApiC_.Models;

namespace proyectoApiC_.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Cliente?> GetByEmailAsync(string email)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email == email);
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
    }
}
