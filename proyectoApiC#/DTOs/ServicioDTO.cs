using System.ComponentModel.DataAnnotations;

namespace proyectoApiC_.DTOs
{
    public class ServicioResponseDTO
    {
        public long IdServicio { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public double Precio { get; set; }
        public int? DuracionEstimada { get; set; }
        public bool Estado { get; set; }
    }

    public class ServicioCreateDTO
    {
        [Required(ErrorMessage = "El código es requerido")]
        [StringLength(20, ErrorMessage = "El código no debe exceder 20 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no debe exceder 100 caracteres")]
        public string Nombre { get; set; }

        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, 99999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        public double Precio { get; set; }

        [Range(0, 1440, ErrorMessage = "La duración estimada debe estar entre 0 y 1440 minutos")]
        public int? DuracionEstimada { get; set; }
    }

    public class ServicioUpdateDTO
    {
        [StringLength(20, ErrorMessage = "El código no debe exceder 20 caracteres")]
        public string? Codigo { get; set; }

        [StringLength(100, ErrorMessage = "El nombre no debe exceder 100 caracteres")]
        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        [Range(0.01, 99999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        public double? Precio { get; set; }

        [Range(0, 1440, ErrorMessage = "La duración estimada debe estar entre 0 y 1440 minutos")]
        public int? DuracionEstimada { get; set; }

        public bool? Estado { get; set; }
    }
}
