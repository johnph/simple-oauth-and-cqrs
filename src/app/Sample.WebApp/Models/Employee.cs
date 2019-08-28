using System;
using System.Collections;
using System.Collections.Generic;

namespace Sample.WebApp.Models
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }

    public class EmployeesResult
    {
        public IEnumerable<Employee> Employees { get; set; }
    }
}
