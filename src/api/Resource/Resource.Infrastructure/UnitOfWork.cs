namespace Resource.Infrastructure
{
    using Resource.Infrastructure.Domain;
    using System.Threading;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeDbContext _employeesContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(
            EmployeeDbContext employeesContext,
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            _employeesContext = employeesContext;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _domainEventsDispatcher.DispatchEventsAsync();
            return await _employeesContext.SaveChangesAsync(cancellationToken);
        }
    }
}
