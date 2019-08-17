namespace Resource.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Resource.Infrastructure.Data.Domain;
    using System;
    using System.Threading.Tasks;

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task CreateAsync(Employee employee)
        {
            await _context.Employee.AddAsync(employee);
        }

        public async Task<Employee> ReadAsync(Guid id)
        {
            return await _context.Employee
                 .SingleAsync(x => x.EmployeeId == id);
        }
    }
}
