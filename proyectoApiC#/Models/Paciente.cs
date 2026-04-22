using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace proyectoApiC_.Models
{
    [Table("pacientes")]
    public class Paciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_paciente")]
        public long IdPaciente { get; set; }

        [Required]
        [StringLength(20)]
        [Column("codigo")]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("especie")]
        public string Especie { get; set; } = string.Empty;

        [StringLength(50)]
        [Column("raza")]
        public string? Raza { get; set; }

        [Column("edad")]
        public int? Edad { get; set; }

        [Column("peso")]
        public double? Peso { get; set; }

        [StringLength(100)]
        [Column("color")]
        public string? Color { get; set; }

        [Column("alergias", TypeName = "TEXT")]
        public string? Alergias { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;

        [Column("id_cliente")]
        public long IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        [JsonIgnore]
        public ICollection<Cita>? Citas { get; set; }
    }
}
