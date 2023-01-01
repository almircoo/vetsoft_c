using proyectoApiC_.Models;

namespace proyectoApiC_.Repositories
{
    public interface IServicioRepository : IRepository<Servicio>
    {
        Task<Servicio?> GetByNombreAsync(string nombre);
        Task<IEnumerable<Servicio>> GetByCodigoAsync(string codigo);
        Task<IEnumerable<Servicio>> GetByRangoPrecionAsync(double precioMin, double precioMax);
        Task<IEnumerable<Servicio>> GetByEstadoAsync(bool estado);
    }
}
