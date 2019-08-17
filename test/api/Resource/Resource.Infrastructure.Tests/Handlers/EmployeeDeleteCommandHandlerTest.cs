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

    public class EmployeeDeleteCommandHandlerTest
    {
        protected EmployeeDeleteCommandHandler EmployeeDeleteCommandHandlerUnderTest { get; }
        protected Mock<IEmployeeRepository> EmployeeRepositoryMock { get; }

        public EmployeeDeleteCommandHandlerTest()
        {
            EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
            EmployeeDeleteCommandHandlerUnderTest = new EmployeeDeleteCommandHandler(EmployeeRepositoryMock.Object);
        }

        public class Handle : EmployeeDeleteCommandHandlerTest
        {
            [Fact]
            public void should_handle_delete_employee_command()
            {
                // Arrange
                var inputCommand =
                    new EmployeeDeleteCommand(anyEmployee.EmployeeId);

                EmployeeRepositoryMock
                    .Setup(i => i.ReadAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(anyEmployee);

                // Act
                var result = EmployeeDeleteCommandHandlerUnderTest.Handle(inputCommand, default(CancellationToken)).Result;

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
