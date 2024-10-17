using GestionEscolarAPP.Data;
using GestionEscolarAPP.Models.Estudiante;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestionEscolarAPP.Controllers.Estudiante
{
    public class CalificacionController(GestionEscolarContext context) : Controller
    {
        private readonly GestionEscolarContext _context = context;

        // GET: Calificaciones
        public async Task<IActionResult> Index()
        {
            int usuarioId = int.Parse(s: User.FindFirst("Id").Value); // Asegúrate de que esto sea correcto

            var calificaciones = await _context.Calificaciones
                .Where(c => c.UsuarioId == usuarioId) // Filtrar por UsuarioId
                .ToListAsync();

            if (calificaciones == null || calificaciones.Count == 0)
            {
                Console.WriteLine("No se encontraron calificaciones."); // Ayuda para depuración
            }


            return View(calificaciones);
        }

        // GET: Calificaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // GET: Calificaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Calificaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Materia,Seccion,UsuarioId,Evaluacion1,Evaluacion2,Evaluacion3")] CalificacionModel calificacion)
        {
            if (ModelState.IsValid)
            {
                // Llamar al procedimiento almacenado para gestionar calificaciones
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_GestionarCalificaciones @Id = NULL, @UsuarioId = {0}, @Materia = {1}, @Seccion = {2}, @Evaluacion1 = {3}, @Evaluacion2 = {4}, @Evaluacion3 = {5}, @Rol = {6}",
                    calificacion.UsuarioId,
                    calificacion.Materia,
                    calificacion.Seccion,
                    calificacion.Evaluacion1,
                    calificacion.Evaluacion2,
                    calificacion.Evaluacion3,
                    "Estudiante" // o "Docente", según corresponda
                );

                return RedirectToAction(nameof(Index));
            }
            return View(calificacion);
        }

        // GET: Calificaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificaciones.FindAsync(id);
            if (calificacion == null)
            {
                return NotFound();
            }
            return View(calificacion);
        }

        // POST: Calificaciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Materia,Seccion,UsuarioId,Evaluacion1,Evaluacion2,Evaluacion3")] CalificacionModel calificacion)
        {
            if (id != calificacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Llamar al procedimiento almacenado para gestionar calificaciones
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC sp_GestionarCalificaciones @Id = {0}, @UsuarioId = {1}, @Materia = {2}, @Seccion = {3}, @Evaluacion1 = {4}, @Evaluacion2 = {5}, @Evaluacion3 = {6}, @Rol = {7}",
                        calificacion.Id,
                        calificacion.UsuarioId,
                        calificacion.Materia,
                        calificacion.Seccion,
                        calificacion.Evaluacion1,
                        calificacion.Evaluacion2,
                        calificacion.Evaluacion3,
                        "Docente" // o "Estudiante", según corresponda
                    );
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalificacionExists(calificacion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(calificacion);
        }

        private bool CalificacionExists(int id)
        {
            return _context.Calificaciones.Any(e => e.Id == id);
        }
    }
}
