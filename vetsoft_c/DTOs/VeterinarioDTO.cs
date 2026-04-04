namespace vetsoft_c.DTOs
{
    public class VeterinarioResponseDTO
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Especialidad { get; set; }
        public string NumeroColegiado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
    }

    public class VeterinarioCreateDTO
    {
        //public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Especialidad { get; set; }
        public string NumeroColegiado { get; set; }
    }

    public class VeterinarioUpdateDTO
    {
        //public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Especialidad { get; set; }
        public string NumeroColegiado { get; set; }
        public bool? Estado { get; set; }
    }
}
