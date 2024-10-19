using System.ComponentModel.DataAnnotations;

namespace GestionEscolarAPP.Models.Estudiante
{
    public class MateriasModel
    {
        [Required]
        public required string Materia { get; set; }

        [Required]
        public required string Dia { get; set; }

        [Required]
        public required string Hora { get; set; }

        [Required]
        public required string Aula { get; set; }

        [Required]
        public required string Maestro { get; set; }
    }
}
