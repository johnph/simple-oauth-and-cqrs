namespace Resource.Infrastructure.Handlers
{
    using Dapper;
    using MediatR;
    using Resource.Infrastructure.Domain;
    using Resource.Infrastructure.Queries;
    using System.Threading;
    using System.Threading.Tasks;

    public class EmployeeReadQueryHandler : IRequestHandler<EmployeeReadQuery, EmployeeResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public EmployeeReadQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<EmployeeResponse> Handle(EmployeeReadQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                                  "[Employee].[EmployeeId], " +
                                  "[Employee].[Name], " +
                                  "[Employee].[Address], " +
                                  "[Employee].[Email] " +
                                  "FROM Resources.Employees AS [Employee] " +
                                  "WHERE [Employee].EmployeeId = @EmployeeId AND [Employee].IsRemoved = 0";

            var employee = await connection.QueryFirstAsync<EmployeeResponse>(sql, new { request.EmployeeId });

            return employee;
        }
    }
}
