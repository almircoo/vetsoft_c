using System.ComponentModel.DataAnnotations;

namespace proyectoApiC_.DTOs
{
    public class VeterinarioResponseDTO
    {
        public long IdVeterinario { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Especialidad { get; set; }
        public string? NumeroColegiado { get; set; }
        public bool Estado { get; set; }
    }

    public class VeterinarioCreateDTO
    {
        [Required(ErrorMessage = "El código es requerido")]
        [StringLength(20, ErrorMessage = "El código no debe exceder 20 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no debe exceder 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(100, ErrorMessage = "El apellido no debe exceder 100 caracteres")]
        public string Apellido { get; set; }

        [EmailAddress(ErrorMessage = "El correo no es válido")]
        [StringLength(150, ErrorMessage = "El correo no debe exceder 150 caracteres")]
        public string? Correo { get; set; }

        [StringLength(20, ErrorMessage = "El teléfono no debe exceder 20 caracteres")]
        public string? Telefono { get; set; }

        [StringLength(100, ErrorMessage = "La especialidad no debe exceder 100 caracteres")]
        public string? Especialidad { get; set; }

        [StringLength(50, ErrorMessage = "El número colegiado no debe exceder 50 caracteres")]
        public string? NumeroColegiado { get; set; }
    }

    public class VeterinarioUpdateDTO
    {
        [StringLength(20, ErrorMessage = "El código no debe exceder 20 caracteres")]
        public string? Codigo { get; set; }

        [StringLength(100, ErrorMessage = "El nombre no debe exceder 100 caracteres")]
        public string? Nombre { get; set; }

        [StringLength(100, ErrorMessage = "El apellido no debe exceder 100 caracteres")]
        public string? Apellido { get; set; }

        [EmailAddress(ErrorMessage = "El correo no es válido")]
        [StringLength(150, ErrorMessage = "El correo no debe exceder 150 caracteres")]
        public string? Correo { get; set; }

        [StringLength(20, ErrorMessage = "El teléfono no debe exceder 20 caracteres")]
        public string? Telefono { get; set; }

        [StringLength(100, ErrorMessage = "La especialidad no debe exceder 100 caracteres")]
        public string? Especialidad { get; set; }

        [StringLength(50, ErrorMessage = "El número colegiado no debe exceder 50 caracteres")]
        public string? NumeroColegiado { get; set; }

        public bool? Estado { get; set; }
    }
}
