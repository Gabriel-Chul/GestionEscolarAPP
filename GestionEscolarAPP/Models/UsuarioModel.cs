using System.ComponentModel.DataAnnotations;

namespace GestionEscolarAPP.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = string.Empty; // Cambiado de "Usuario" a "NombreUsuario"
        public string Clave { get; set; } = string.Empty; // Cambiado de "Contraseña" a "Clave"
        public string Rol { get; set; } = string.Empty; // Puede ser "Estudiante" o "Docente"
    }
}
