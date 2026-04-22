using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace proyectoApiC_.Models
{
    [Table("veterinarios")]
    public class Veterinario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_veterinario")]
        public long IdVeterinario { get; set; }

        [Required]
        [StringLength(20)]
        [Column("codigo")]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [StringLength(50)]
        [Column("numero_colegiado")]
        public string? NumeroColegiado { get; set; }

        [StringLength(100)]
        [Column("especialidad")]
        public string? Especialidad { get; set; }

        [StringLength(150)]
        [Column("correo")]
        public string? Correo { get; set; }

        [StringLength(20)]
        [Column("telefono")]
        public string? Telefono { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;

        [JsonIgnore]
        public ICollection<Cita>? Citas { get; set; }
    }
}
