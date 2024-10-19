using Microsoft.AspNetCore.Mvc;
using GestionEscolarAPP.Models.Estudiante;
using System.Collections.Generic;

namespace GestionEscolarAPP.Controllers.Estudiante
{
    [Route("Estudiante/[action]")]
    public class MateriasController : Controller
    {
        [HttpGet]
        public IActionResult Materias()
        {
            var model = new List<MateriasModel>
            {
                new() { Materia = "Matemáticas", Dia = "Lunes y Miércoles", Hora = "10:00 - 11:30", Aula = "Aula 101", Maestro = "Prof. Juan Pérez" },
                new() { Materia = "Lenguaje", Dia = "Martes y Jueves", Hora = "09:00 - 10:30", Aula = "Aula 102", Maestro = "Prof. María López" },
                new() { Materia = "Ciencias", Dia = "Lunes y Miércoles", Hora = "12:00 - 13:30", Aula = "Aula 201", Maestro = "Prof. Carlos Fernández" },
                new() { Materia = "Química", Dia = "Martes y Jueves", Hora = "11:00 - 12:30", Aula = "Aula 202", Maestro = "Prof. Ana Martínez" },
                new() { Materia = "Sociales", Dia = "Viernes", Hora = "09:00 - 11:00", Aula = "Aula 203", Maestro = "Prof. Luis Gómez" }
            };

            // Especifica la ruta completa de la vista
            return View("~/Views/Estudiante/Materias.cshtml", model);
        }
    }
}
