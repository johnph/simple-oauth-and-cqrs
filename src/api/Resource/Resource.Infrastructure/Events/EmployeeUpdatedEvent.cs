namespace Resource.Infrastructure.Events
{
    using Resource.Infrastructure.Data.Domain;
    using Resource.Infrastructure.Domain;

    public class EmployeeUpdatedEvent : DomainEventBase
    {
        public Employee Employee { get; }

        public EmployeeUpdatedEvent(Employee employee)
        {
            Employee = employee;
        }
    }
}
