namespace Resource.Infrastructure.Tests.Handlers
{
    using MediatR;
    using Moq;
    using Resource.Infrastructure.Commands;
    using Resource.Infrastructure.Data.Domain;
    using Resource.Infrastructure.Handlers;
    using Resource.Infrastructure.Repositories;
    using System;
    using System.Threading;
    using Xunit;

    public class EmployeeUpdateCommandHandlerTest
    {
        protected EmployeeUpdateCommandHandler EmployeeUpdateCommandHandlerUnderTest { get; }
        protected Mock<IEmployeeRepository> EmployeeRepositoryMock { get; }

        public EmployeeUpdateCommandHandlerTest()
        {
            EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
            EmployeeUpdateCommandHandlerUnderTest = new EmployeeUpdateCommandHandler(EmployeeRepositoryMock.Object);
        }

        public class Handle : EmployeeUpdateCommandHandlerTest
        {
            [Fact]
            public void should_handle_update_employee_command()
            {
                // Arrange
                var inputCommand = 
                    new EmployeeUpdateCommand(anyEmployee.EmployeeId, "joe don", "bedford", "joed@email.com");
                
                EmployeeRepositoryMock
                    .Setup(i => i.ReadAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(anyEmployee);

                // Act
                var result = EmployeeUpdateCommandHandlerUnderTest.Handle(inputCommand, default(CancellationToken)).Result;

                // Assert
                Assert.Equal(Unit.Value, result);
            }

            private Employee anyEmployee => new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                Name = "joe",
                Address = "bedford",
                Email = "joe@email.com"
            };
        }
    }
}
