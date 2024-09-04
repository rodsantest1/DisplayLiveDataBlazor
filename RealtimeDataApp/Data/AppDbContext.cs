using Microsoft.EntityFrameworkCore;

namespace RealtimeDataApp.Data
{
    public class AppDbContext : DbContext
    {
        string _connectionString = "Server=.;Database=HamMgmtDB;Trusted_Connection=True;";

        public DbSet<Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
