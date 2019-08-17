namespace Resource.Infrastructure.Domain
{
    using System.Threading.Tasks;

    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
