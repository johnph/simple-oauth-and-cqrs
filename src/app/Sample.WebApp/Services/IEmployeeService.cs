using Sample.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.WebApp.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> Get();
        Task<Employee> Get(Guid id);
        Task Add(Employee employees);
        Task Update(Employee employees);
        Task Delete(Guid id);
    }
}
