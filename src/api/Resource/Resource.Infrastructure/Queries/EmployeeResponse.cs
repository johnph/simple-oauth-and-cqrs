namespace Resource.Infrastructure.Queries
{
    using System;
    using System.Collections.Generic;

    public class EmployeeResponse
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }

    public class EmployeesResponse
    {
        public IEnumerable<EmployeeResponse> Employees { get; set; }
    }
}
