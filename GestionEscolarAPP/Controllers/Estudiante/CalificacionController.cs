using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using GestionEscolarAPP.Models.Estudiante;
using GestionEscolarAPP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

public class CalificacionesController : Controller
{
    private readonly GestionEscolarContext _context;

    public CalificacionesController(GestionEscolarContext context)
    {
        _context = context;
    }

    // Acción que ejecuta el SP y pasa los datos a la vista
    public async Task<IActionResult> VerCalificaciones()
    {
        // Obtener el ID del estudiante desde el usuario autenticado (nombre de usuario)
        var usuario = User.Identity.Name;  // Obtener el nombre de usuario
        var estudianteId = await _context.Usuarios
                                         .Where(u => u.Usuario == usuario)
                                         .Select(u => u.Id)
                                         .FirstOrDefaultAsync();  // Obtiene el ID del estudiante

        if (estudianteId == 0)
        {
            // Si no se encuentra el estudiante, se maneja con un error o una página personalizada
            return NotFound("Estudiante no encontrado.");
        }

        // Ejecutar el SP para obtener las calificaciones del estudiante
        var calificaciones = await _context.Calificaciones
                                           .FromSqlRaw("EXEC sp_ObtenerCalificacionesPivot @EstudianteID = {0}", estudianteId)
                                           .ToListAsync();

        // Si no se encuentran calificaciones, puedes manejarlo de otra forma
        if (calificaciones == null || !calificaciones.Any())
        {
            // Puede ser útil retornar un mensaje si no hay calificaciones
            return NotFound("No se encontraron calificaciones.");
        }

        // Retornar la vista con los datos de las calificaciones
        return View(calificaciones);
    }
}


