namespace Resource.Infrastructure.Domain
{
    using MediatR;
    using System;

    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
