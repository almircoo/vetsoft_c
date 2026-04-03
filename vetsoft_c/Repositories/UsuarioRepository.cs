using Microsoft.EntityFrameworkCore;
using vetsoft_c.Data;
using vetsoft_c.Models;

namespace vetsoft_c.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Usuario?> GetByCodigoAsync(string codigo)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Codigo == codigo);
        }

        public async Task<Usuario?> GetByCorreoAsync(string correo)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<IEnumerable<Usuario>> GetByRolAsync(Rol rol)
        {
            string rolString = rol.ToString();

            return await _dbSet.AsNoTracking()
                .Where(u => u.RolString == rolString)
                .ToListAsync();
        }
    }
}