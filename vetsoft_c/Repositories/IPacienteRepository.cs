using vetsoft_c.Models;

namespace vetsoft_c.Repositories
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        Task<IEnumerable<Paciente>> GetByNombreAsync(string nombre);
        Task<IEnumerable<Paciente>> GetByClienteIdAsync(long clienteId);
        Task<IEnumerable<Paciente>> GetByCodigoAsync(string codigo);
        Task<IEnumerable<Paciente>> GetByEspecieAsync(string especie);
        Task<IEnumerable<Paciente>> GetByEstadoAsync(bool estado);
    }
}
