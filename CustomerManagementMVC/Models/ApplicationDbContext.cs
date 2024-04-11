using Microsoft.EntityFrameworkCore;

namespace CustomerManagementMVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected void OnModelCreate(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
