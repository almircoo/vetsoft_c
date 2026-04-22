using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace proyectoApiC_.Models
{
    [Table("clientes")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_cliente")]
        public long IdCliente { get; set; }

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

        [StringLength(150)]
        [Column("correo")]
        public string? Correo { get; set; }

        [StringLength(20)]
        [Column("telefono")]
        public string? Telefono { get; set; }

        [StringLength(255)]
        [Column("direccion")]
        public string? Direccion { get; set; }

        [StringLength(100)]
        [Column("ciudad")]
        public string? Ciudad { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;

        [JsonIgnore]
        public ICollection<Paciente>? Pacientes { get; set; }
    }
}
