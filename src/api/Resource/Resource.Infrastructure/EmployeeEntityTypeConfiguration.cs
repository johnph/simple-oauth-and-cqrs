using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Resource.Infrastructure
{
    using Resource.Infrastructure.Data.Domain;

    internal class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees", SchemaNames.Employees);
            builder.HasKey(t => t.EmployeeId);
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.Address).IsRequired();
            builder.Property(t => t.Email).IsRequired();
        }
    }
}
