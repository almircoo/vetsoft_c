namespace proyectoApiC_.DTOs
{
    public class UsuarioResponseDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }

    public class UsuarioCreateDTO
    {
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; } = "USUARIO";
    }

    public class UsuarioUpdateDTO
    {
        public string? NombreUsuario { get; set; }
        public string? Email { get; set; }
        public string? Rol { get; set; }
    }

    public class UsuarioLoginDTO
    {
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
    }
}
