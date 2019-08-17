namespace Resource.Infrastructure.Events
{
    using Resource.Infrastructure.Data.Domain;
    using Resource.Infrastructure.Domain;

    public class EmployeeDeletedEvent : DomainEventBase
    {
        public Employee Employee { get; }

        public EmployeeDeletedEvent(Employee employee)
        {
            Employee = employee;
        }
    }
}
