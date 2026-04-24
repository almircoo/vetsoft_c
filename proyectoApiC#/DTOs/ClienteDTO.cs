using System.ComponentModel.DataAnnotations;

namespace proyectoApiC_.DTOs
{
    public class ClienteResponseDTO
    {
        public long IdCliente { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public bool Estado { get; set; }
    }

    public class ClienteCreateDTO
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

        [StringLength(255, ErrorMessage = "La dirección no debe exceder 255 caracteres")]
        public string? Direccion { get; set; }

        [StringLength(100, ErrorMessage = "La ciudad no debe exceder 100 caracteres")]
        public string? Ciudad { get; set; }
    }

    public class ClienteUpdateDTO
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

        [StringLength(255, ErrorMessage = "La dirección no debe exceder 255 caracteres")]
        public string? Direccion { get; set; }

        [StringLength(100, ErrorMessage = "La ciudad no debe exceder 100 caracteres")]
        public string? Ciudad { get; set; }

        public bool? Estado { get; set; }
    }
}
