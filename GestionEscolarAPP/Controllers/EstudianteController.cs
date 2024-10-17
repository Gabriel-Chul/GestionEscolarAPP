using Microsoft.AspNetCore.Mvc;
using GestionEscolarAPP.Controllers.Filters;

namespace GestionEscolarAPP.Controllers
{
    [ServiceFilter(typeof(AutenticacionFilter))] // Aplica el filtro de autenticación
    public class EstudianteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // GET: Estudiante/Calificaciones
        public IActionResult Calificaciones()
        {
            // Aquí deberías obtener las calificaciones del estudiante desde la base de datos
            return View(); // Asegúrate de que la vista 'Calificaciones.cshtml' existe
        }
    }
}
