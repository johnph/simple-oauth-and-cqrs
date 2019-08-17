namespace Resource.Infrastructure.Handlers
{
    using MediatR;
    using Resource.Infrastructure.Commands;
    using Resource.Infrastructure.Repositories;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class EmployeeUpdateCommandHandler : IRequestHandler<EmployeeUpdateCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeUpdateCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));            
        }

        public async Task<Unit> Handle(EmployeeUpdateCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.ReadAsync(request.EmployeeId);

            employee.Change(request.Name, request.Address, request.Email);

            return Unit.Value;
        }
    }
}
