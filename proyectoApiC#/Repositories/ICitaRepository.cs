using proyectoApiC_.Models;

namespace proyectoApiC_.Repositories
{
    public interface ICitaRepository : IRepository<Cita>
    {
        Task<IEnumerable<Cita>> GetByVeterinarioIdAsync(long veterinarioId);
        Task<IEnumerable<Cita>> GetByPacienteIdAsync(long pacienteId);
        Task<IEnumerable<Cita>> GetByFechaAsync(DateTime fecha);
        Task<IEnumerable<Cita>> GetByEstadoAsync(string estado);
        Task<IEnumerable<Cita>> GetByServicioIdAsync(long servicioId);
        Task<IEnumerable<Cita>> GetByCodigoAsync(string codigo);
        Task<IEnumerable<Cita>> GetByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin);
    }
}
