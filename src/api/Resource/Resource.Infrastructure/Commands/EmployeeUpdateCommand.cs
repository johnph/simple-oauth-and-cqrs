namespace Resource.Infrastructure.Commands
{
    using MediatR;
    using System;

    public class EmployeeUpdateCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public EmployeeUpdateCommand(Guid employeeId, string name, string address, string email)
        {
            EmployeeId = employeeId;
            Name = name;
            Address = address;
            Email = email;
        }
    }
}
