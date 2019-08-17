namespace Resource.Infrastructure.Handlers
{
    using MediatR;
    using Resource.Infrastructure.Commands;
    using Resource.Infrastructure.Data.Domain;
    using Resource.Infrastructure.Repositories;
    using Resource.Infrastructure.Domain;
    using System.Threading;
    using System.Threading.Tasks;

    public class EmployeeCreateCommandHandler : IRequestHandler<EmployeeCreateCommand, EmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeCreateCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<EmployeeCommand> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee(request.Name, request.Address, request.Email);

            await _employeeRepository.CreateAsync(employee);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new EmployeeCommand() { Id = employee.EmployeeId };
        }
    }
}
