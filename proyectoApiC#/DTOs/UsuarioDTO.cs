namespace proyectoApiC_.DTOs
{
    public class UsuarioResponseDTO
    {
        public long Id { get; set; } 
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; } 
        public string Rol { get; set; }
        public bool Estado { get; set; }
    }

    public class UsuarioCreateDTO
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; } = "USUARIO";
    }

    public class UsuarioLoginDTO
    {
        public string Correo { get; set; }
        public string Contrasena { get; set; }
    }
}