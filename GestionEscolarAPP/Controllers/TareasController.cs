using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionEscolarAPP.Data;
using GestionEscolarAPP.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GestionEscolarAPP.Controllers
{
    public class TareasController : Controller
    {
        private readonly GestionEscolarContext _context;

        public TareasController(GestionEscolarContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View("Views/Docente/Tareas/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                await _context.InsertarTareaAsync(tarea); // Llamar al método del contexto
                return RedirectToAction("ViewVista");
            }
            return View("Views/Docente/Tareas/Create.cshtml", tarea);
        }

        public async Task<IActionResult> ViewVista()
        {
            var tareas = await _context.ObtenerTareasAsync();
            return View("Views/Docente/Tareas/ViewVista.cshtml", tareas);
        }
    }
}
