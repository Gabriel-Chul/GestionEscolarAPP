using GestionEscolarAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace GestionEscolarAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Accediendo a la página de inicio.");
            return View();
        }

        public IActionResult About()
        {
            _logger.LogInformation("Accediendo a la página 'Acerca de Nosotros'.");
            return View("About");
        }

        public IActionResult Courses()
        {
            _logger.LogInformation("Accediendo a la página de cursos.");
            return View("Courses");
        }

        public IActionResult Admissions()
        {
            _logger.LogInformation("Accediendo a la página de admisiones.");
            return View("Admissions");
        }

        public IActionResult Contact()
        {
            _logger.LogInformation("Accediendo a la página de contacto.");
            return View("Contact");
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Accediendo a la página de privacidad.");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("Se produjo un error.");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
