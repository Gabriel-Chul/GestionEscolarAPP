using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GestionEscolarAPP.Middleware
{
    public class NoCacheMiddleware
    {
        private readonly RequestDelegate _next;

        public NoCacheMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Establecer encabezados para prevenir caché solo si no están presentes
            if (!context.Response.Headers.ContainsKey("Cache-Control"))
            {
                context.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1
            }

            if (!context.Response.Headers.ContainsKey("Pragma"))
            {
                context.Response.Headers.Append("Pragma", "no-cache"); // HTTP 1.0
            }

            if (!context.Response.Headers.ContainsKey("Expires"))
            {
                context.Response.Headers.Append("Expires", "-1"); // Proxies
            }

            await _next(context);
        }
    }
}
