using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace proyectoApiC_.Models
{
    [Table("servicios")]
    public class Servicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_servicio")]
        public long IdServicio { get; set; }

        [Required]
        [StringLength(20)]
        [Column("codigo")]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("descripcion", TypeName = "TEXT")]
        public string? Descripcion { get; set; }

        [Required]
        [Column("precio")]
        public double Precio { get; set; }

        [Column("duracion_estimada")]
        public int? DuracionEstimada { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;

        [JsonIgnore]
        public ICollection<Cita>? Citas { get; set; }
    }
}
