using proyectoApiC_.Models;

namespace proyectoApiC_.Repositories
{
    public interface IServicioRepository : IRepository<Servicio>
    {
        Task<Servicio?> GetByNombreAsync(string nombre);
        Task<IEnumerable<Servicio>> GetByTipoAsync(string tipo);
    }
}
