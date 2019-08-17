namespace Resource.Infrastructure.Events
{
    using Resource.Infrastructure.Data.Domain;
    using Resource.Infrastructure.Domain;

    public class EmployeeCreatedEvent : DomainEventBase
    {
        public Employee Employee { get; }

        public EmployeeCreatedEvent(Employee employee)
        {
            Employee = employee;
        }
    }
}
