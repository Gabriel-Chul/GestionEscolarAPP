using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionEscolarAPP.Models.Docente; // Importar el namespace correcto para el modelo
using GestionEscolarAPP.Data;
using System.Linq;
using System.Threading.Tasks;
using GestionEscolarAPP.Controllers.Filters;
using Microsoft.Data.SqlClient; // Importar para el uso de SqlParameter
using Microsoft.AspNetCore.Http; // Importar para el uso de HttpContext.Session

namespace GestionEscolarAPP.Controllers
{
    [ServiceFilter(typeof(AutenticacionFilter))] // Aplica el filtro de autenticación
    public class DocenteController : Controller
    {
        private readonly GestionEscolarContext _context;

        public DocenteController(GestionEscolarContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Acción para mostrar las calificaciones de los estudiantes asociados
        public async Task<IActionResult> Calificaciones()
        {
            // Ejecutar el SP para obtener las calificaciones de los estudiantes
            var calificaciones = await _context.CalificacionesDocente
                .FromSqlRaw("EXEC sp_ObtenerEstudiantesCalificaciones")
                .ToListAsync();

            // Pasa las calificaciones a la vista
            return View(calificaciones);
        }
    }
}
