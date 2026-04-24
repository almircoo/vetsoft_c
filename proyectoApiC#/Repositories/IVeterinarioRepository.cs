using proyectoApiC_.Models;

namespace proyectoApiC_.Repositories
{
    public interface IVeterinarioRepository : IRepository<Veterinario>
    {
        Task<Veterinario?> GetByEmailAsync(string email);
        Task<IEnumerable<Veterinario>> GetByNombreAsync(string nombre);
        Task<IEnumerable<Veterinario>> GetByEspecialidadAsync(string especialidad);
    }
}
