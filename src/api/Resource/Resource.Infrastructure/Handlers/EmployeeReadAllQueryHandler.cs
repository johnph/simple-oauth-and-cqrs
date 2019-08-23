namespace Resource.Infrastructure.Handlers
{
    using Dapper;
    using MediatR;
    using Resource.Infrastructure.Domain;
    using Resource.Infrastructure.Queries;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class EmployeeReadAllQueryHandler : IRequestHandler<EmployeeReadAllQuery, EmployeesResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public EmployeeReadAllQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<EmployeesResponse> Handle(EmployeeReadAllQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                                  "[Employee].[EmployeeId], " +
                                  "[Employee].[Name], " +
                                  "[Employee].[Address], " +
                                  "[Employee].[Email] " +
                                  "FROM Resources.Employees AS [Employee] " +
                                  "WHERE [Employee].IsRemoved = 0";

            var employees = await connection.QueryAsync<EmployeeResponse>(sql);

            return new EmployeesResponse() { Employees = employees };
        }
    }
}
