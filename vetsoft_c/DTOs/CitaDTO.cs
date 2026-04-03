namespace vetsoft_c.DTOs
{
    public class CitaResponseDTO
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaHora { get; set; }
        public string? Motivo { get; set; }
        public string? Notas { get; set; }
        public string? Diagnostico { get; set; }
        public string? Tratamiento { get; set; }
        public string Estado { get; set; }
        public long PacienteId { get; set; }
        public long VeterinarioId { get; set; }
        public long ServicioId { get; set; }
    }

    public class CitaCreateDTO
    {
        public string Codigo { get; set; }
        public DateTime FechaHora { get; set; }
        public string? Motivo { get; set; }
        public long PacienteId { get; set; }
        public long VeterinarioId { get; set; }
        public long ServicioId { get; set; }
    }

    public class CitaUpdateDTO
    {
        public string? Codigo { get; set; }
        public DateTime? FechaHora { get; set; }
        public string? Motivo { get; set; }
        public string? Notas { get; set; }
        public string? Diagnostico { get; set; }
        public string? Tratamiento { get; set; }
        public string? Estado { get; set; }
        public long? PacienteId { get; set; }
        public long? VeterinarioId { get; set; }
        public long? ServicioId { get; set; }
    }
}
