namespace vetsoft_c.DTOs
{
    public class PacienteResponseDTO
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string? Raza { get; set; }
        public int? Edad { get; set; }
        public double? Peso { get; set; }
        public string? Color { get; set; }
        public string? Alergias { get; set; }
        public bool Estado { get; set; }
        public long ClienteId { get; set; }
    }

    public class PacienteCreateDTO
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string? Raza { get; set; }
        public int? Edad { get; set; }
        public double? Peso { get; set; }
        public string? Color { get; set; }
        public string? Alergias { get; set; }
        public long ClienteId { get; set; }
    }

    public class PacienteUpdateDTO
    {
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Especie { get; set; }
        public string? Raza { get; set; }
        public int? Edad { get; set; }
        public double? Peso { get; set; }
        public string? Color { get; set; }
        public string? Alergias { get; set; }
        public bool? Estado { get; set; }
        public long? ClienteId { get; set; }
    }
}
