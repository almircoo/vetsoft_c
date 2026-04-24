using proyectoApiC_.Models;

namespace proyectoApiC_.Repositories
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        Task<IEnumerable<Paciente>> GetByNombreAsync(string nombre);
        Task<IEnumerable<Paciente>> GetByClienteIdAsync(int clienteId);
    }
}
