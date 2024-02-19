using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Infrastructure;

namespace craftersmine.Valknut.Server
{
    public class ValknutDatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL();

            base.OnConfiguring(optionsBuilder);
        }
    }
}
