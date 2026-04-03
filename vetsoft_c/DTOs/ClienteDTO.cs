namespace vetsoft_c.DTOs
{
    public class ClienteResponseDTO
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
    }

    public class ClienteCreateDTO
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }

    public class ClienteUpdateDTO
    {
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public bool? Estado { get; set; }
    }
}
