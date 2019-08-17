namespace Resource.Infrastructure.Domain
{
    using System.Data;

    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
