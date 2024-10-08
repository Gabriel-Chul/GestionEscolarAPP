using GestionEscolarAPP.Controllers.Filters;
using GestionEscolarAPP.Data;
using GestionEscolarAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GestionEscolarAPP.Controllers
{
    [ServiceFilter(typeof(AutenticacionFilter))] // Aplica el filtro de autenticación
    public class AccountController : Controller
    {
        private readonly GestionEscolarContext _context;
        private readonly ILogger<AccountController> _logger; // Inyección de logger

        public AccountController(GestionEscolarContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Acción para mostrar la página de login
        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                // Verificar si el usuario ya está autenticado
                if (HttpContext.Session.GetString("Usuario") != null)
                {
                    // Redirigir a la página correspondiente según el rol
                    string rol = HttpContext.Session.GetString("Rol") ?? "RolDesconocido";
                    return rol == "Estudiante" ? RedirectToAction("Index", "Estudiante") : RedirectToAction("Index", "Docente");
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar la página de login.");
                ModelState.AddModelError("", "Se ha producido un error al cargar la página de inicio de sesión.");
                return View();
            }
        }

        // Acción para procesar el login
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Validar que no estén vacíos
                    if (string.IsNullOrEmpty(model.Usuario) || string.IsNullOrEmpty(model.Clave))
                    {
                        ModelState.AddModelError("", "Por favor, complete todos los campos.");
                        return View(model);
                    }

                    // Lógica para validar las credenciales del usuario
                    var usuario = ValidarCredenciales(model.Usuario, model.Clave);
                    if (usuario != null)
                    {
                        // Asegurarse de que 'Usuario' y 'Rol' no sean nulos
                        string nombreUsuario = usuario.Usuario ?? "Usuario Desconocido";
                        string rol = usuario.Rol ?? "RolDesconocido";

                        HttpContext.Session.SetString("Usuario", nombreUsuario);
                        HttpContext.Session.SetString("Rol", rol);

                        return rol switch
                        {
                            "Estudiante" => RedirectToAction("Index", "Estudiante"),
                            "Docente" => RedirectToAction("Index", "Docente"),
                            _ => RedirectToAction("Login")
                        };
                    }
                    else
                    {
                        ModelState.AddModelError("", "Usuario o clave incorrectos");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al procesar el inicio de sesión.");
                    ModelState.AddModelError("", "Se ha producido un error al intentar iniciar sesión.");
                }
            }

            return View(model);
        }

        // Acción para cerrar sesión
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear(); // Limpiar la sesión
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cerrar sesión.");
                ModelState.AddModelError("", "Se ha producido un error al cerrar sesión.");
                return RedirectToAction("Login");
            }
        }

        // Método para validar las credenciales del usuario
        private UsuarioModel? ValidarCredenciales(string usuario, string clave)
        {
            try
            {
                List<UsuarioModel> usuarios = new();
                var usuarioParam = new SqlParameter("@Usuario", usuario);
                var claveParam = new SqlParameter("@Clave", clave);

                // Obtener los usuarios que coinciden con las credenciales
                usuarios = _context.Usuarios
                    .FromSqlRaw("EXEC sp_ValidarUsuario @Usuario, @Clave", usuarioParam, claveParam)
                    .ToList();

                return usuarios.FirstOrDefault(); // Devuelve el primer usuario encontrado o nulo
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar las credenciales del usuario.");
                return null;
            }
        }
    }
}
