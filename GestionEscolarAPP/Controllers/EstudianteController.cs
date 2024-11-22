using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionEscolarAPP.Models;  // Asegúrate de usar el namespace correcto para tus modelos
using GestionEscolarAPP.Data;    // Asegúrate de usar el namespace correcto para tu contexto de base de datos
using System.Linq;
using System.Threading.Tasks;
using GestionEscolarAPP.Controllers.Filters;
using Microsoft.Data.SqlClient;

namespace GestionEscolarAPP.Controllers
{
    [ServiceFilter(typeof(AutenticacionFilter))] // Aplica el filtro de autenticación
    public class EstudianteController : Controller
    {
        private readonly GestionEscolarContext _context;

        public EstudianteController(GestionEscolarContext context)
        {
            _context = context;
        }

        // Acción para mostrar las calificaciones del estudiante
        public async Task<IActionResult> Calificaciones()
        {
            // Obtener el ID del estudiante desde la sesión
            var estudianteId = HttpContext.Session.GetInt32("EstudianteID");

            if (!estudianteId.HasValue)
            {
                // Si no hay ID de estudiante en la sesión, redirigir al login
                return RedirectToAction("Login", "Account");
            }

            // Ejecutar el SP para obtener las calificaciones usando el EstudianteID
            var calificaciones = await _context.Calificaciones
                .FromSqlRaw("EXEC sp_ObtenerCalificacionesPivot @EstudianteID",
                            new SqlParameter("@EstudianteID", estudianteId.Value))
                .ToListAsync();

            // Pasa las calificaciones a la vista
            return View(calificaciones);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
