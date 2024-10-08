using System;

namespace GestionEscolarAPP.Models
{
    public class NotificacionModel
    {
        public int Id { get; set; }

        // Agrega el modificador "required"
        public required string Mensaje { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }
    }
}
