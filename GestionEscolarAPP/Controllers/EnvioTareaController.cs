using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionEscolarAPP.Data;
using System.Threading.Tasks;
using GestionEscolarAPP.Models;

namespace GestionEscolarAPP.Controllers
{
    public class EnvioTareaController : Controller
    {
        private readonly GestionEscolarContext _context;

        public EnvioTareaController(GestionEscolarContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Enviar(int tareaId, string comentario, string enlace)
        {
            // Verificar que el enlace no esté vacío
            if (string.IsNullOrWhiteSpace(enlace))
            {
                ModelState.AddModelError("enlace", "El enlace es obligatorio.");
                return View("~/Views/Estudiante/Tareas/Enviar.cshtml"); // Retornar a la vista de enviar con el error
            }

            // Verificar si la tarea existe antes de intentar insertar
            var tareaExistente = await _context.Tareas.FindAsync(tareaId);
            if (tareaExistente == null)
            {
                ModelState.AddModelError("", "La tarea especificada no existe.");
                return View("~/Views/Estudiante/Tareas/Enviar.cshtml"); // Retornar a la vista de enviar con el error
            }

            // Crear una nueva instancia de Entrega y asignar valores
            var entrega = new Entrega
            {
                Id = tareaId, // Asignar el Id de la tarea
                Comentario = comentario,
                Enlace = enlace
            };

            // Llamar al método que ejecuta el procedimiento almacenado
            await _context.EnviarTareaAsync(entrega.Id, entrega.Comentario, entrega.Enlace);

            return RedirectToAction("Vista1"); // Redirigir a Vista1 o a donde sea apropiado
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
