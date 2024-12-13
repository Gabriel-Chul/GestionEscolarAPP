namespace GestionEscolarAPP.Models.Docente
{
    public class CalificacionDocenteModel
    {
        public int CalificacioID { get; set; }
        public int EstudianteID { get; set; }
        public string? EstudianteNombre { get; set; } // Permitir nulos
        public int SeccionID { get; set; }
        public string? SeccionNombre { get; set; } // Permitir nulos
        public int MateriaID { get; set; }
        public string? MateriaNombre { get; set; } // Permitir nulos
        public int PeriodoID { get; set; }
        public decimal Nota { get; set; }
        public DateTime FechaCalificacion { get; set; }
        public decimal? TotalNota { get; set; }
    }
}
