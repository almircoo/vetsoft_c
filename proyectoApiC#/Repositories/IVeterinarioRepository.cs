using proyectoApiC_.Models;

namespace proyectoApiC_.Repositories
{
    public interface IVeterinarioRepository : IRepository<Veterinario>
    {
        Task<Veterinario?> GetByCorreoAsync(string correo);
        Task<IEnumerable<Veterinario>> GetByNombreAsync(string nombre);
        Task<IEnumerable<Veterinario>> GetByEspecialidadAsync(string especialidad);
        Task<IEnumerable<Veterinario>> GetByCodigoAsync(string codigo);
        Task<IEnumerable<Veterinario>> GetByEstadoAsync(bool estado);
    }
}
