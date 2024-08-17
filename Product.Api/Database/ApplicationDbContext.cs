using Microsoft.EntityFrameworkCore;

namespace Product.Api.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Common.Models.Product> Products { get; set; }
    }
}
