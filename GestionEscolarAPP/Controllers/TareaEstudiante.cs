using Microsoft.AspNetCore.Mvc;
using GestionEscolarAPP.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using GestionEscolarAPP.Data;
using System.Threading;
using System.Linq;

namespace GestionEscolarAPP.Controllers
{
    public class TareaEstudiante : Controller
    {
        private readonly GestionEscolarContext _context;

        public TareaEstudiante(GestionEscolarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Vista1()
        {
            var tareas = await _context.Tareas.ToListAsync();

            // Convertir Tarea a TareaEstudiante
            var tareasEstudiante = tareas.Select(t => new GestionEscolarAPP.Models.TareaEstudiante
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Materia = t.Materia,
                FechaEntrega = t.FechaEntrega,
                Documento = t.Documento
            }).ToList();

            return View("~/Views/Estudiante/Tareas/Vista1.cshtml", tareasEstudiante);
        }

        public IActionResult Enviar()
        {
            return View("~/Views/Estudiante/Tareas/Enviar.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Enviar(int Id, string comentario, string enlace)
        {
            // Verificar que el enlace no esté vacío
            if (string.IsNullOrWhiteSpace(enlace))
            {
                ModelState.AddModelError("enlace", "El enlace es obligatorio.");
                return View("~/Views/Estudiante/Tareas/Enviar.cshtml"); // Retornar a la vista de enviar con el error
            }

            // Aquí podrías tener alguna lógica para determinar cuál tarea se está enviando,
            // o se puede almacenar en la base de datos sin un id específico como se discutió.

            // Puedes necesitar obtener una tarea existente de alguna manera si tienes lógica de negocio para eso.
            // Por ejemplo:
            // var tareaId = // lógica para determinar la tarea que se está enviando
             await _context.EnviarTareaAsync(Id, comentario, enlace);

            // Si no se utiliza tareaId, puedes comentar la línea anterior y manejarlo de acuerdo a tus requisitos.

            //await _context.EnviarTareaAsync(comentario, enlace); // Usa el método actualizado.

            return RedirectToAction("Vista1"); // Redirigir a Vista1 o a donde sea apropiado
        }
    }
}
