namespace Resource.Infrastructure.Data.Domain
{
    using Resource.Infrastructure.Domain;
    using Resource.Infrastructure.Events;
    using System;

    public class Employee : Entity, IAggregateRoot
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsRemoved { get; set; }

        public Employee()
        {

        }

        public Employee(string name, string address, string email)
        {
            EmployeeId = Guid.NewGuid();
            Name = name;
            Address = address;
            Email = email;

            AddDomainEvent(new EmployeeCreatedEvent(this));
        }

        public void Change(string name, string address, string email)
        {
            Name = name;
            Address = address;
            Email = email;

            AddDomainEvent(new EmployeeUpdatedEvent(this));
        }

        public void Remove()
        {
            IsRemoved = true;

            AddDomainEvent(new EmployeeDeletedEvent(this));
        }
    }
}
