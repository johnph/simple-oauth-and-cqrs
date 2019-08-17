using MediatR;

namespace Resource.Infrastructure.Commands
{
    public class EmployeeCreateCommand : IRequest<EmployeeCommand>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public EmployeeCreateCommand(string name, string address, string email)
        {
            Name = name;
            Address = address;
            Email = email;
        }
    }
}
