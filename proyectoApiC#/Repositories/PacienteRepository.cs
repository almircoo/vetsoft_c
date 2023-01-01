using Microsoft.EntityFrameworkCore;
using proyectoApiC_.Data;
using proyectoApiC_.Models;

namespace proyectoApiC_.Repositories
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Paciente>> GetByNombreAsync(string nombre)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.Nombre.Contains(nombre))
                .ToListAsync();
        }

        public async Task<IEnumerable<Paciente>> GetByClienteIdAsync(long clienteId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.IdCliente == clienteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Paciente>> GetByCodigoAsync(string codigo)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.Codigo.Contains(codigo))
                .ToListAsync();
        }

        public async Task<IEnumerable<Paciente>> GetByEspecieAsync(string especie)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.Especie == especie)
                .ToListAsync();
        }

        public async Task<IEnumerable<Paciente>> GetByEstadoAsync(bool estado)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.Estado == estado)
                .ToListAsync();
        }
    }
}
