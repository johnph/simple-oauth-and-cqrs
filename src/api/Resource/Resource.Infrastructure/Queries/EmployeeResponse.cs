namespace Resource.Infrastructure.Queries
{
    using System;

    public class EmployeeResponse
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
