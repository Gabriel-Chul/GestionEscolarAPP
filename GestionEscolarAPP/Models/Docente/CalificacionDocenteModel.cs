namespace GestionEscolarAPP.Models.Docente
{
    public class CalificacionDocenteModel
    {
        public int CalificacioID { get; set; }
        public int EstudianteID { get; set; }
        public string? EstudianteNombre { get; set; } // Permitir nulos
        public string? SeccionNombre { get; set; } // Permitir nulos
        public string? MateriaNombre { get; set; } // Permitir nulos
        public decimal Periodo1 { get; set; }
        public decimal Periodo2 { get; set; }
        public decimal Periodo3 { get; set; }
        public decimal TotalNota { get; set; }
    }
}
