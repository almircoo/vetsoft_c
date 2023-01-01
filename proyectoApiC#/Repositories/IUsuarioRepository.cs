using proyectoApiC_.Models;

namespace proyectoApiC_.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> GetByCodigoAsync(string codigo);
        Task<Usuario?> GetByCorreoAsync(string correo);
        Task<IEnumerable<Usuario>> GetByRolAsync(Rol rol);
    }
}