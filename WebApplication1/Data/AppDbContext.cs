using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    // ========================================================
    // DBCONTEXT
    //
    // É a ponte entre nossa aplicação C# e o banco.
    // ========================================================

    public class AppDbContext : DbContext
    {
        // ====================================================
        // RECEBE AS CONFIGURAÇÕES PELO DEPENDENCY INJECTION
        // ====================================================

        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }


        // ====================================================
        // DbSet
        //
        // Representa os dados da tabela Clientes.
        //
        // Podemos pensar:
        //
        // DbSet<Cliente>
        //       ↓
        // tabela Clientes
        // ====================================================

        public DbSet<Cliente> Clientes { get; set; } = null!;
    }
}