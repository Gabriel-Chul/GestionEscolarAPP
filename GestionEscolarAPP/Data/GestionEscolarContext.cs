using GestionEscolarAPP.Models;
using GestionEscolarAPP.Models.Docente;
using GestionEscolarAPP.Models.Estudiante;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace GestionEscolarAPP.Data
{
    public class GestionEscolarContext : DbContext
    {
        public GestionEscolarContext(DbContextOptions<GestionEscolarContext> options)
            : base(options)
        {
        }

        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Entrega> EntregasTareas { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<CalificacionModel> Calificaciones { get; set; }
        public DbSet<CalificacionDocenteModel> CalificacionesDocente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>().ToTable("Usuarios");

            modelBuilder.Entity<CalificacionModel>()
                .HasNoKey()
                .ToView(null);

            modelBuilder.Entity<CalificacionDocenteModel>(entity =>
            {
                entity.ToTable("Calificaciones");
                entity.HasKey(e => e.CalificacioID);
                entity.Property(e => e.TotalNota).HasColumnType("DECIMAL(3,1)");
                entity.Property(e => e.EstudianteNombre).HasMaxLength(100);
                entity.Property(e => e.SeccionNombre).HasMaxLength(50);
                entity.Property(e => e.MateriaNombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Entrega>(entity =>
            {
                entity.ToTable("EntregasTareas");
                entity.HasKey(e => e.Id);
                entity.HasOne<Tarea>()
                      .WithMany()
                      .HasForeignKey(e => e.Id)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public async Task InsertarTareaAsync(Tarea tarea)
        {
            var parameters = new[]
            {
                new SqlParameter("@Titulo", tarea.Titulo),
                new SqlParameter("@Descripcion", tarea.Descripcion),
                new SqlParameter("@Materia", tarea.Materia),
                new SqlParameter("@FechaEntrega", tarea.FechaEntrega),
                new SqlParameter("@Documento", tarea.Documento)
            };

            await Database.ExecuteSqlRawAsync("EXEC sp_InsertarTarea @Titulo, @Descripcion, @Materia, @FechaEntrega, @Documento", parameters);
        }

        public async Task<List<Tarea>> ObtenerTareasAsync()
        {
            return await Tareas.ToListAsync();
        }

        public async Task EnviarTareaAsync(int tareaId, string comentario, string enlace)
        {
            var parameters = new[]
            {
                new SqlParameter("@TareaID", tareaId),
                new SqlParameter("@Comentario", (object)comentario ?? DBNull.Value),
                new SqlParameter("@ArchivoURL", enlace)
            };

            await Database.ExecuteSqlRawAsync("EXEC sp_EnviarEntrega @TareaID, @Comentario, @ArchivoURL", parameters);
        }
    }
}
