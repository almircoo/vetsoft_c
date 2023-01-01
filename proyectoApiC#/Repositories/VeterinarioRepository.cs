using Microsoft.EntityFrameworkCore;
using proyectoApiC_.Data;
using proyectoApiC_.Models;

namespace proyectoApiC_.Repositories
{
    public class VeterinarioRepository : Repository<Veterinario>, IVeterinarioRepository
    {
        public VeterinarioRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Veterinario?> GetByCorreoAsync(string correo)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Correo == correo);
        }

        public async Task<IEnumerable<Veterinario>> GetByNombreAsync(string nombre)
        {
            return await _dbSet.AsNoTracking()
                .Where(v => v.Nombre.Contains(nombre))
                .ToListAsync();
        }

        public async Task<IEnumerable<Veterinario>> GetByEspecialidadAsync(string especialidad)
        {
            return await _dbSet.AsNoTracking()
                .Where(v => v.Especialidad == especialidad)
                .ToListAsync();
        }

        public async Task<IEnumerable<Veterinario>> GetByCodigoAsync(string codigo)
        {
            return await _dbSet.AsNoTracking()
                .Where(v => v.Codigo.Contains(codigo))
                .ToListAsync();
        }

        public async Task<IEnumerable<Veterinario>> GetByEstadoAsync(bool estado)
        {
            return await _dbSet.AsNoTracking()
                .Where(v => v.Estado == estado)
                .ToListAsync();
        }
    }
}
