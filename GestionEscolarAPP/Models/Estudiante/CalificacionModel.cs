using System.ComponentModel.DataAnnotations;

namespace GestionEscolarAPP.Models.Estudiante
{
    public class CalificacionModel
    {
        // Propiedad para el ID, que no puede ser nula
        [Key]
        public int Id { get; set; }

        // Propiedad para la materia, que no puede ser nula
        [Required(ErrorMessage = "La materia es obligatoria.")]
        public string Materia { get; set; } = string.Empty; // Valor predeterminado

        // Propiedad para la sección, que no puede ser nula
        [Required(ErrorMessage = "La sección es obligatoria.")]
        public string Seccion { get; set; } = string.Empty; // Valor predeterminado

        // Propiedad para el ID del usuario, que no puede ser nula
        [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
        public int UsuarioId { get; set; }

        // Propiedad para Evaluación 1
        public decimal Evaluacion1 { get; set; }

        // Propiedad para Evaluación 2
        public decimal Evaluacion2 { get; set; }

        // Propiedad para Evaluación 3
        public decimal Evaluacion3 { get; set; }

        // Propiedad para Nota Final
        public decimal NotaFinal
        {
            get
            {
                // Calcular la nota final como el promedio de las evaluaciones
                if (Evaluacion1 == 0 && Evaluacion2 == 0 && Evaluacion3 == 0)
                {
                    return 0; // Retornar 0 si no hay evaluaciones
                }
                return (Evaluacion1 + Evaluacion2 + Evaluacion3) / 3;
            }
        }

        // Constructor por defecto (opcional)
        public CalificacionModel() { }
    }
}
