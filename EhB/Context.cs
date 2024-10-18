using EhB;
using Microsoft.EntityFrameworkCore;

namespace EhB
{
    public class Context : DbContext
    {
        public DbSet<Clothe> Clothes { get; set; }

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres");
        }
    }
}
