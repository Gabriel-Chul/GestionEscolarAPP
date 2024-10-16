using Microsoft.AspNetCore.Mvc;
using GestionEscolarAPP.Models;
using System.Collections.Generic;

namespace GestionEscolarAPP.Controllers
{
    public class EstadisticasController : Controller
    {
        // Simulación de una base de datos en memoria
        private static List<EstadisticaEstudiante> estadisticas = new List<EstadisticaEstudiante>();

        // GET: Estadisticas/Index
        public IActionResult Index()
        {
            // El docente puede ver las estadísticas de todos los estudiantes registrados
            return View(estadisticas);
        }

        // GET: Estadisticas/Crear
        public IActionResult Crear()
        {
            // El docente verá el formulario para registrar estadísticas de un estudiante
            return View();
        }

        // POST: Estadisticas/GuardarEstadisticas
        [HttpPost]
        public IActionResult GuardarEstadisticas(EstadisticaEstudiante estadistica)
        {
            if (ModelState.IsValid)
            {
                // Simulamos que agregamos la estadística a la base de datos
                estadisticas.Add(estadistica);

                // Redirige a la página de estadísticas donde el docente puede ver lo que ha registrado
                return RedirectToAction("Index");
            }

            // Si hay un error de validación, vuelve al formulario
            return View("Crear", estadistica);
        }
    }
}
