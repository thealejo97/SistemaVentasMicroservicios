using Microsoft.EntityFrameworkCore;
using ClientesService.Models;

namespace ClientesService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
