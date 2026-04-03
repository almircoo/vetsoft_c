using vetsoft_c.Models;

namespace vetsoft_c.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente?> GetByCorreoAsync(string correo);
        Task<Cliente?> GetByTelefonoAsync(string telefono);
        Task<IEnumerable<Cliente>> GetByNombreAsync(string nombre);
        Task<IEnumerable<Cliente>> GetByCodigoAsync(string codigo);
        Task<IEnumerable<Cliente>> GetByEstadoAsync(bool estado);
    }
}
