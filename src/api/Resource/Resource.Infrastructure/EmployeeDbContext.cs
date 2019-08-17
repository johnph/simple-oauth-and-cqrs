namespace Resource.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Resource.Infrastructure.Data.Domain;

    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }

        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
        }
    }
}
