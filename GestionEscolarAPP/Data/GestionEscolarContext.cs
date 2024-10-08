using GestionEscolarAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEscolarAPP.Data
{
    public class GestionEscolarContext : DbContext
    {
        public GestionEscolarContext(DbContextOptions<GestionEscolarContext> options)
            : base(options)
        {
        }

        // Define tus DbSets aquí
        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}
