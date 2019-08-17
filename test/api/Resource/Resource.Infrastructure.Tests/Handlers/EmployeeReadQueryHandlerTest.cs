using Dapper;
using Moq;
using Resource.Infrastructure.Data.Domain;
using Resource.Infrastructure.Domain;
using Resource.Infrastructure.Handlers;
using Resource.Infrastructure.Queries;
using System;
using System.Data;
using System.Threading;
using Xunit;

namespace Resource.Infrastructure.Tests.Handlers
{
    public class EmployeeReadQueryHandlerTest
    {
        protected EmployeeReadQueryHandler EmployeeReadQueryHandlerUnderTest { get; }
        protected Mock<ISqlConnectionFactory> SqlConnectionFactoryMock { get; }

        protected Mock<IDbConnection> DbConnectionMock { get; }

        public EmployeeReadQueryHandlerTest()
        {
            SqlConnectionFactoryMock = new Mock<ISqlConnectionFactory>();
            DbConnectionMock = new Mock<IDbConnection>();
            EmployeeReadQueryHandlerUnderTest = new EmployeeReadQueryHandler(SqlConnectionFactoryMock.Object);
        }

        public class Handle : EmployeeReadQueryHandlerTest
        {
            // should use some in-memory database for unit testing this query part
            //[Fact]
            public void should_read_employee()
            {
                // Arrange
                var inputQuery = new EmployeeReadQuery(anyEmployee.EmployeeId);

                SqlConnectionFactoryMock.Setup(i => i.GetOpenConnection())
                    .Returns(DbConnectionMock.Object);
                
                // Act
                var result = EmployeeReadQueryHandlerUnderTest.Handle(inputQuery, default(CancellationToken)).Result;

                // Assert
                Assert.Equal(inputQuery.EmployeeId, result.EmployeeId);
                Assert.Equal(anyEmployee.Name, result.Name);
            }

            private EmployeeResponse anyEmployee => new EmployeeResponse()
            {
                EmployeeId = Guid.NewGuid(),
                Name = "joe",
                Address = "bedford",
                Email = "joe@email.com"
            };
        }
    }
}
