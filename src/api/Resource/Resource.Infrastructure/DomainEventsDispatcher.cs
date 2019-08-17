namespace Resource.Infrastructure
{
    using Autofac;
    using MediatR;
    using Resource.Infrastructure.Domain;
    using System.Linq;
    using System.Threading.Tasks;

    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ILifetimeScope _scope;
        private readonly EmployeeDbContext _context;

        public DomainEventsDispatcher(IMediator mediator, ILifetimeScope scope, EmployeeDbContext context)
        {
            _mediator = mediator;
            _scope = scope;
            _context = context;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEntities = _context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await _mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
