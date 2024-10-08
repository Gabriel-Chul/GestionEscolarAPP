using GestionEscolarAPP.Controllers.Filters;
using GestionEscolarAPP.Data;
using GestionEscolarAPP.Middleware;
using Microsoft.EntityFrameworkCore;

namespace GestionEscolarAPP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Agregar servicios al contenedor.
            builder.Services.AddDbContext<GestionEscolarContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Registrar el filtro de autenticaci�n
            builder.Services.AddScoped<AutenticacionFilter>();

            var app = builder.Build();

            // Configurar la tuber�a de solicitud HTTP.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseMiddleware<NoCacheMiddleware>(); // A�adir el middleware NoCache aqu�
            app.UseSession(); // A�adir el middleware de sesiones
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}