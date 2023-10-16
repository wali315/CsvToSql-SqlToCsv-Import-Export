using Microsoft.EntityFrameworkCore;

namespace ConsoleApp3
{
    public class CsvDbContext : DbContext
    {
        public DbSet<CsvModel> CsvModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            MyConfiguration config = new MyConfiguration();
            var connection = config.GetConnectionString();
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
