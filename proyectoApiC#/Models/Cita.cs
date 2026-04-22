using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoApiC_.Models
{
    [Table("citas")]
    public class Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_cita")]
        public long IdCita { get; set; }

        [Required]
        [StringLength(20)]
        [Column("codigo")]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        [Column("fecha_hora")]
        public DateTime FechaHora { get; set; }

        [StringLength(255)]
        [Column("motivo")]
        public string? Motivo { get; set; }

        [Column("notas", TypeName = "TEXT")]
        public string? Notas { get; set; }

        [Column("diagnostico", TypeName = "TEXT")]
        public string? Diagnostico { get; set; }

        [Column("tratamiento", TypeName = "TEXT")]
        public string? Tratamiento { get; set; }

        [StringLength(50)]
        [Column("estado")]
        public string Estado { get; set; } = "PROGRAMADA";

        [Column("id_paciente")]
        public long IdPaciente { get; set; }

        [ForeignKey("IdPaciente")]
        public Paciente? Paciente { get; set; }

        [Column("id_veterinario")]
        public long IdVeterinario { get; set; }

        [ForeignKey("IdVeterinario")]
        public Veterinario? Veterinario { get; set; }

        [Column("id_servicio")]
        public long IdServicio { get; set; }

        [ForeignKey("IdServicio")]
        public Servicio? Servicio { get; set; }
    }
}
