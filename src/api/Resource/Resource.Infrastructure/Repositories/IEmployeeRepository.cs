using Resource.Infrastructure.Data.Domain;
namespace Resource.Infrastructure.Repositories
{
    using System;
    using System.Threading.Tasks;

    public interface IEmployeeRepository
    {
        Task CreateAsync(Employee employee);
        Task<Employee> ReadAsync(Guid id);
    }
}
