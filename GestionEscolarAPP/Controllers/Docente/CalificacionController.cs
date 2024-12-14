using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionEscolarAPP.Models.Docente; // Importar el namespace correcto para el modelo
using GestionEscolarAPP.Data;
using System.Linq;
using System.Threading.Tasks;
using GestionEscolarAPP.Controllers.Filters;
using Microsoft.Data.SqlClient; // Importar para el uso de SqlParameter
using Microsoft.AspNetCore.Http;

namespace GestionEscolarAPP.Controllers.Docente
{
    [ServiceFilter(typeof(AutenticacionFilter))] // Aplica el filtro de autenticación
    public class CalificacionController : Controller
    {
        private readonly GestionEscolarContext _context;

        public CalificacionController(GestionEscolarContext context)
        {
            _context = context;
        }

        // Acción para mostrar las calificaciones de los estudiantes
        public async Task<IActionResult> Calificaciones()
        {
            // Ejecutar el SP para obtener las calificaciones de los estudiantes
            var calificaciones = await _context.CalificacionesDocente
                .FromSqlRaw("EXEC sp_ObtenerEstudiantesCalificaciones")
                .ToListAsync();

            // Pasa las calificaciones a la vista
            return View(calificaciones);
        }

        public IActionResult Index()
        {
            return View();
        }

        // Métodos para crear, editar y eliminar calificaciones
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CalificacionDocenteModel calificacion)
        {
            if (ModelState.IsValid)
            {
                _context.CalificacionesDocente.Add(calificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Calificaciones)); // Redirigir a la acción Calificaciones
            }
            return View(calificacion);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.CalificacionesDocente.FindAsync(id);
            if (calificacion == null)
            {
                return NotFound();
            }
            return View(calificacion);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, CalificacionDocenteModel calificacion)
        {
            if (id != calificacion.CalificacioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calificacion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Calificaciones)); // Redirigir a la acción Calificaciones
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalificacionExists(calificacion.CalificacioID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(calificacion);
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.CalificacionesDocente
                .FirstOrDefaultAsync(m => m.CalificacioID == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var calificacion = await _context.CalificacionesDocente.FindAsync(id);
            if (calificacion != null) // Asegúrate de que calificacion no es nulo
            {
                _context.CalificacionesDocente.Remove(calificacion);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Calificaciones)); // Redirigir a la acción Calificaciones
        }


        private bool CalificacionExists(int id)
        {
            return _context.CalificacionesDocente.Any(e => e.CalificacioID == id);
        }
    }
}
