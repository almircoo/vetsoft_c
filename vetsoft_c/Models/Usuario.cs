using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vetsoft_c.Models
{
    public enum Rol
    {
        ADMIN,
        VETERINARIO,
        USUARIO
    }

    [Table("usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_usuario")]
        public long IdUsuario { get; set; }

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

        [Required]
        [StringLength(150)]
        [Column("correo")]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [Column("contrasena")]
        public string Contrasena { get; set; } = string.Empty;

        [Required]
        [Column("rol")]
        [StringLength(20)]
        public string RolString 
        { 
            get { return Rol.ToString(); }
            set { Rol = Enum.Parse<Rol>(value); }
        }

        [NotMapped]
        public Rol Rol { get; set; } = Rol.USUARIO;

        [Column("estado")]
        public bool Estado { get; set; } = true;
    }
}
