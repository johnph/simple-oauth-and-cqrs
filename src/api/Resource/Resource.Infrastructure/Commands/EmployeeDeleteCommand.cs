namespace Resource.Infrastructure.Commands
{
    using MediatR;
    using System;

    public class EmployeeDeleteCommand : IRequest
    {
        public Guid EmployeeId { get; set; }

        public EmployeeDeleteCommand(Guid employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
