namespace GestionEscolarAPP.Models
{
    public class EstadisticaEstudiante
    {
        public int Id { get; set; }

        public string Estudiante { get; set; }

        public int DiasAsistidos { get; set; }

        public int TareasCumplidas { get; set; }

        public int TareasNoCumplidas { get; set; }

        // Podrías agregar más campos si es necesario, por ejemplo, si un docente quiere agregar comentarios
        //public string ComentariosDocente { get; set; }
    }
}
