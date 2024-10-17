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

        // Define tus DbSets aquí
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<CalificacionModel> Calificaciones { get; set; } // Agregado para manejar calificaciones

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración adicional para tus modelos, si es necesario
            modelBuilder.Entity<CalificacionModel>()
                .ToTable("Calificaciones"); // Asegúrate que esto coincida con el nombre de tu tabla en la base de datos

            // Configura otras entidades si es necesario
            modelBuilder.Entity<UsuarioModel>()
                .ToTable("Usuarios"); // Asegúrate que esto coincida con el nombre de tu tabla en la base de datos
        }
    }
}
