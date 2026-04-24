using proyectoApiC_.Models;

namespace proyectoApiC_.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente?> GetByEmailAsync(string email);
        Task<Cliente?> GetByTelefonoAsync(string telefono);
        Task<IEnumerable<Cliente>> GetByNombreAsync(string nombre);
    }
}
