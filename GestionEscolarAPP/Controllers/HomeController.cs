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
            _logger.LogInformation("Accediendo a la p�gina de inicio.");
            return View();
        }

        public IActionResult About()
        {
            _logger.LogInformation("Accediendo a la p�gina 'Acerca de Nosotros'.");
            return View("About");
        }

        public IActionResult Courses()
        {
            _logger.LogInformation("Accediendo a la p�gina de cursos.");
            return View("Courses");
        }

        public IActionResult Admissions()
        {
            _logger.LogInformation("Accediendo a la p�gina de admisiones.");
            return View("Admissions");
        }

        public IActionResult Contact()
        {
            _logger.LogInformation("Accediendo a la p�gina de contacto.");
            return View("Contact");
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Accediendo a la p�gina de privacidad.");
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
