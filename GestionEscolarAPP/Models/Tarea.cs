using System.ComponentModel.DataAnnotations;

namespace GestionEscolarAPP.Models
{
    public class Tarea
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La materia es obligatoria.")]
        public string? Materia { get; set; }

        [Required(ErrorMessage = "La fecha de entrega es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaEntrega { get; set; }

        [Required(ErrorMessage = "El documento es obligatorio.")]
        public string? Documento { get; set; }
    }
}
//public class EnvioTarea
//{
//    public int Id { get; set; }
//    public int TareaId { get; set; }
//    public string Comentario { get; set; }
//    public string Enlace { get; set; }
//    public DateTime FechaEnvio { get; set; }
//}

//public class Tarea1
//{
//    public int Id { get; set; }
//    public string Titulo { get; set; }
//    public string Descripcion { get; set; }
//    public string Materia { get; set; }
//    public DateTime FechaEntrega { get; set; }
//    public string Documento { get; set; }
//}