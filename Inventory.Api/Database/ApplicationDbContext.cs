using Microsoft.EntityFrameworkCore;

namespace Inventory.Api.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Common.Models.Product> ProductsTbl { get; set; }
    }
}
