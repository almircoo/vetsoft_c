using System.ComponentModel.DataAnnotations;

namespace proyectoApiC_.DTOs
{
    public class CitaResponseDTO
    {
        public long IdCita { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaHora { get; set; }
        public string? Motivo { get; set; }
        public string? Notas { get; set; }
        public string? Diagnostico { get; set; }
        public string? Tratamiento { get; set; }
        public string Estado { get; set; }
        public long IdPaciente { get; set; }
        public long IdVeterinario { get; set; }
        public long IdServicio { get; set; }
        public PacienteResponseDTO? Paciente { get; set; }
        public VeterinarioResponseDTO? Veterinario { get; set; }
        public ServicioResponseDTO? Servicio { get; set; }
    }

    public class CitaCreateDTO
    {
        [Required(ErrorMessage = "El código es requerido")]
        [StringLength(20, ErrorMessage = "El código no debe exceder 20 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "La fecha y hora son requeridas")]
        public DateTime FechaHora { get; set; }

        [StringLength(255, ErrorMessage = "El motivo no debe exceder 255 caracteres")]
        public string? Motivo { get; set; }

        [Required(ErrorMessage = "El ID del paciente es requerido")]
        public long IdPaciente { get; set; }

        [Required(ErrorMessage = "El ID del veterinario es requerido")]
        public long IdVeterinario { get; set; }

        [Required(ErrorMessage = "El ID del servicio es requerido")]
        public long IdServicio { get; set; }
    }

    public class CitaUpdateDTO
    {
        [StringLength(20, ErrorMessage = "El código no debe exceder 20 caracteres")]
        public string? Codigo { get; set; }

        public DateTime? FechaHora { get; set; }

        [StringLength(255, ErrorMessage = "El motivo no debe exceder 255 caracteres")]
        public string? Motivo { get; set; }

        public string? Notas { get; set; }

        public string? Diagnostico { get; set; }

        public string? Tratamiento { get; set; }

        [StringLength(50, ErrorMessage = "El estado no debe exceder 50 caracteres")]
        public string? Estado { get; set; }

        public long? IdPaciente { get; set; }

        public long? IdVeterinario { get; set; }

        public long? IdServicio { get; set; }
    }
}
