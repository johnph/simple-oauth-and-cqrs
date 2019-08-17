namespace Resource.Infrastructure.Handlers
{
    using MediatR;
    using Resource.Infrastructure.Commands;
    using Resource.Infrastructure.Repositories;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class EmployeeDeleteCommandHandler : IRequestHandler<EmployeeDeleteCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeDeleteCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<Unit> Handle(EmployeeDeleteCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.ReadAsync(request.EmployeeId);

            employee.Remove();

            return Unit.Value;
        }
    }
}
