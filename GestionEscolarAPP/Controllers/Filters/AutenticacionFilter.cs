using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GestionEscolarAPP.Controllers.Filters
{
    public class AutenticacionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Verificar si el usuario está autenticado
            var usuario = context.HttpContext.Session.GetString("Usuario");

            // Verificar si el controlador y la acción son 'Account/Login'
            bool isLoginAction = context.ActionDescriptor.RouteValues["controller"] == "Account" &&
                                 context.ActionDescriptor.RouteValues["action"] == "Login";

            // Redirigir solo si el usuario no está autenticado y no está intentando acceder a la página de login
            if (string.IsNullOrEmpty(usuario) && !isLoginAction)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
            else
            {
                Console.WriteLine($"Usuario autenticado: {usuario}");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Lógica después de que se ejecuta la acción, si es necesaria
        }
    }
}
