using Microsoft.EntityFrameworkCore;
using proyectoApiC_.Models;

namespace proyectoApiC_.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Veterinario> Veterinarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cita> Citas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // You can add additional configuration here if data annotations are not enough
            modelBuilder.Entity<Usuario>()
                .Property(u => u.RolString)
                .HasConversion<string>();
        }
    }
}
