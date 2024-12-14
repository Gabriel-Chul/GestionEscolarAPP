using GestionEscolarAPP.Controllers;
using GestionEscolarAPP.Models;
using GestionEscolarAPP.Models.Docente;  // Asegúrate de tener el using para el modelo Docente
using GestionEscolarAPP.Models.Estudiante;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Threading;

namespace GestionEscolarAPP.Data
{
    public class GestionEscolarContext : DbContext
    {
        public GestionEscolarContext(DbContextOptions<GestionEscolarContext> options)
            : base(options)
        {
        }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Models.TareaEstudiante> Tarea { get; set; } // Agregando DbSet para EnvioTarea//PRUEBA


        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<CalificacionModel> Calificaciones { get; set; }
        public DbSet<CalificacionDocenteModel> CalificacionesDocente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración del modelo de Usuario
            modelBuilder.Entity<UsuarioModel>()
                .ToTable("Usuarios");

            // Configuración del modelo de Calificación de Estudiantes (sin clave primaria)
            modelBuilder.Entity<CalificacionModel>()
                .HasNoKey()  // Indica que CalificacionModel no tiene clave primaria
                .ToView(null); // Indica que no está mapeado a una vista si no corresponde a una tabla directa.

            // Configuración del modelo de Calificación de Docentes
            modelBuilder.Entity<CalificacionDocenteModel>(entity =>
            {
                entity.ToTable("Calificaciones");  // Mapear a la tabla Calificaciones
                entity.HasKey(e => e.CalificacioID);  // Nombre correcto de la columna
     
                entity.Property(e => e.TotalNota)
                    .HasColumnType("DECIMAL(3,1)");  // Configura el tipo de datos para TotalNota
                entity.Property(e => e.EstudianteNombre)
                    .HasMaxLength(100);  // Configura el tipo de datos para EstudianteNombre
                entity.Property(e => e.SeccionNombre)
                    .HasMaxLength(50);  // Configura el tipo de datos para SeccionNombre
                entity.Property(e => e.MateriaNombre)
                    .HasMaxLength(100);  // Configura el tipo de datos para MateriaNombre
            });
        }

        //
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

        // Método para obtener todas las tareas
        public async Task<List<Tarea>> ObtenerTareasAsync()
        {
            return await Tareas.ToListAsync();
        }

        internal async Task EnviarTareaAsync(int tareaId, string comentario, string enlace)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", tareaId),
                new SqlParameter("@Comentario", (object)comentario ?? DBNull.Value),
                new SqlParameter("@Enlace", enlace)
    };

            await Database.ExecuteSqlRawAsync("EXEC sp_EnviarEntrega @Id, @Comentario, @Enlace", parameters);
        }
        //
    }
}
