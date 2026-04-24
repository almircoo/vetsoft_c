namespace proyectoApiC_.DTOs
{
    public class ServicioResponseDTO
    {
        public long Id { get; set; }
        public string Codigo { get; set; };
        public string Nombre { get; set; };
        public string? Descripcion { get; set; }
        public double Precio { get; set; }
        public int? DuracionEstimada { get; set; }
        public bool Estado { get; set; }
    }

    public class ServicioCreateDTO
    {
        public string Codigo { get; set; };
        public string Nombre { get; set; };
        public string? Descripcion { get; set; }
        public double Precio { get; set; }
        public int? DuracionEstimada { get; set; }
    }

    public class ServicioUpdateDTO
    {
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public double? Precio { get; set; }
        public int? DuracionEstimada { get; set; }
        public bool? Estado { get; set; }
    }
}
