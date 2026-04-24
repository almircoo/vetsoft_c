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

        public async Task<IEnumerable<Paciente>> GetByClienteIdAsync(int clienteId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.ClienteId == clienteId)
                .ToListAsync();
        }
    }
}
