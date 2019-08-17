namespace Resource.Infrastructure.Queries
{
    using MediatR;
    using System;

    public class EmployeeReadQuery : IRequest<EmployeeResponse>
    {
        public Guid EmployeeId { get; }

        public EmployeeReadQuery(Guid employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
