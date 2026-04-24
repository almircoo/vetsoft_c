using System.ComponentModel.DataAnnotations;

namespace proyectoApiC_.DTOs
{
    public class PacienteResponseDTO
    {
        public long IdPaciente { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string? Raza { get; set; }
        public int? Edad { get; set; }
        public double? Peso { get; set; }
        public string? Color { get; set; }
        public string? Alergias { get; set; }
        public bool Estado { get; set; }
        public long IdCliente { get; set; }
        public ClienteResponseDTO? Cliente { get; set; }
    }

    public class PacienteCreateDTO
    {
        [Required(ErrorMessage = "El código es requerido")]
        [StringLength(20, ErrorMessage = "El código no debe exceder 20 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no debe exceder 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La especie es requerida")]
        [StringLength(50, ErrorMessage = "La especie no debe exceder 50 caracteres")]
        public string Especie { get; set; }

        [StringLength(50, ErrorMessage = "La raza no debe exceder 50 caracteres")]
        public string? Raza { get; set; }

        [Range(0, 150, ErrorMessage = "La edad debe estar entre 0 y 150")]
        public int? Edad { get; set; }

        [Range(0.01, 1000, ErrorMessage = "El peso debe estar entre 0.01 y 1000")]
        public double? Peso { get; set; }

        [StringLength(100, ErrorMessage = "El color no debe exceder 100 caracteres")]
        public string? Color { get; set; }

        public string? Alergias { get; set; }

        [Required(ErrorMessage = "El Id del cliente es requerido")]
        public long IdCliente { get; set; }
    }

    public class PacienteUpdateDTO
    {
        [StringLength(20, ErrorMessage = "El código no debe exceder 20 caracteres")]
        public string? Codigo { get; set; }

        [StringLength(100, ErrorMessage = "El nombre no debe exceder 100 caracteres")]
        public string? Nombre { get; set; }

        [StringLength(50, ErrorMessage = "La especie no debe exceder 50 caracteres")]
        public string? Especie { get; set; }

        [StringLength(50, ErrorMessage = "La raza no debe exceder 50 caracteres")]
        public string? Raza { get; set; }

        [Range(0, 150, ErrorMessage = "La edad debe estar entre 0 y 150")]
        public int? Edad { get; set; }

        [Range(0.01, 1000, ErrorMessage = "El peso debe estar entre 0.01 y 1000")]
        public double? Peso { get; set; }

        [StringLength(100, ErrorMessage = "El color no debe exceder 100 caracteres")]
        public string? Color { get; set; }

        public string? Alergias { get; set; }

        public bool? Estado { get; set; }

        public long? IdCliente { get; set; }
    }
}
