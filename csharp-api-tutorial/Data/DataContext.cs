using Microsoft.EntityFrameworkCore;

namespace csharp_api_tutorial.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options ):base(options) { }


        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
