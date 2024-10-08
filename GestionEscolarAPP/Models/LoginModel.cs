using System.ComponentModel.DataAnnotations;

namespace GestionEscolarAPP.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder los 50 caracteres.")]
        public required string Usuario { get; set; }

        [Required(ErrorMessage = "El campo Clave es obligatorio.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "La clave debe tener al menos {2} caracteres.", MinimumLength = 6)]
        public required string Clave { get; set; }
    }
}
