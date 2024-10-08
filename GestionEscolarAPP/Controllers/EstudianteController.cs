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
    }
}
