namespace GestionEscolarAPP.Models
{
    public class TareaEstudiante
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Materia { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Documento { get; set; }

    }
}