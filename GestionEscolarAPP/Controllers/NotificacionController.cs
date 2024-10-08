using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using GestionEscolarAPP.Models;

namespace GestionEscolarAPP.Controllers
{
    public class NotificacionController : Controller
    {
        // Método para obtener notificaciones
        public IActionResult ObtenerNotificaciones()
        {
            // Simulando una lista de notificaciones
            var notificaciones = new List<NotificacionModel>
            {
                new NotificacionModel { Id = 1, Mensaje = "Tienes una nueva tarea pendiente.", Fecha = DateTime.Now },
                new NotificacionModel { Id = 2, Mensaje = "Reunión programada para mañana.", Fecha = DateTime.Now },
                new NotificacionModel { Id = 3, Mensaje = "Resultados de la última evaluación disponibles.", Fecha = DateTime.Now }
            };

            return Json(notificaciones);
        }
    }
}
