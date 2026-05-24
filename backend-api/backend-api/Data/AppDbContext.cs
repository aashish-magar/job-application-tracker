using Microsoft.EntityFrameworkCore;

namespace backend_api.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Application> Applications { get; set; }
        public DbSet<Models.StatusHistory> StatusHistories { get; set; }
    }

}
