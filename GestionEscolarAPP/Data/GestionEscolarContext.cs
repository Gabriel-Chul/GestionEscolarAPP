using GestionEscolarAPP.Models;
using GestionEscolarAPP.Models.Estudiante;
using Microsoft.EntityFrameworkCore;

namespace GestionEscolarAPP.Data
{
    public class GestionEscolarContext : DbContext
    {
        public GestionEscolarContext(DbContextOptions<GestionEscolarContext> options)
            : base(options)
        {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<CalificacionModel> Calificaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Si CalificacionModel no tiene clave primaria, indica esto aquí
            modelBuilder.Entity<CalificacionModel>()
                .HasNoKey()  // Esto indica que CalificacionModel no tiene clave primaria
                .ToView(null); // También le puedes indicar que no está mapeado a una vista si no corresponde a una tabla directa.

            modelBuilder.Entity<UsuarioModel>()
                .ToTable("Usuarios");

            modelBuilder.Entity<CalificacionModel>(entity =>
            {
                entity.Property(e => e.Periodo1)
                    .HasColumnType("DECIMAL(5,2)");  // Configura el tipo de datos para Periodo1

                entity.Property(e => e.Periodo2)
                    .HasColumnType("DECIMAL(5,2)");  // Configura el tipo de datos para Periodo2

                entity.Property(e => e.Periodo3)
                    .HasColumnType("DECIMAL(5,2)");  // Configura el tipo de datos para Periodo3

                entity.Property(e => e.Total)
                    .HasColumnType("DECIMAL(3,1)");  // Configura el tipo de datos para Total
            });
        }


    }

}
