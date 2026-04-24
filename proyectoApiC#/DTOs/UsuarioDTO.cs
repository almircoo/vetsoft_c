using System.ComponentModel.DataAnnotations;

namespace proyectoApiC_.DTOs
{
    public class UsuarioResponseDTO
    {
        public long IdUsuario { get; set; } 
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; } 
        public string Rol { get; set; }
        public bool Estado { get; set; }
    }

    public class UsuarioCreateDTO
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

        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        [StringLength(150, ErrorMessage = "El correo no debe exceder 150 caracteres")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(255, ErrorMessage = "La contraseña no debe exceder 255 caracteres")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        [StringLength(20, ErrorMessage = "El rol no debe exceder 20 caracteres")]
        public string Rol { get; set; } = "USUARIO";
    }

    public class UsuarioUpdateDTO
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

        [StringLength(255, ErrorMessage = "La contraseña no debe exceder 255 caracteres")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string? Contrasena { get; set; }

        [StringLength(20, ErrorMessage = "El rol no debe exceder 20 caracteres")]
        public string? Rol { get; set; }

        public bool? Estado { get; set; }
    }

    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Contrasena { get; set; }
    }
}
