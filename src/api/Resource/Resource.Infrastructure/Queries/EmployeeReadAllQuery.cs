using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resource.Infrastructure.Queries
{
    public class EmployeeReadAllQuery : IRequest<EmployeesResponse>
    {
        public EmployeeReadAllQuery()
        {
            
        }
    }
}
