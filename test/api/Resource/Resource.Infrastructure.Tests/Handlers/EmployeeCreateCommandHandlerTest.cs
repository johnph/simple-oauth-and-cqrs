namespace Resource.Infrastructure.Tests.Handlers
{
    using Moq;
    using Resource.Infrastructure.Commands;
    using Resource.Infrastructure.Data.Domain;
    using Resource.Infrastructure.Domain;
    using Resource.Infrastructure.Handlers;
    using Resource.Infrastructure.Repositories;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class EmployeeCreateCommandHandlerTest
    {
        protected EmployeeCreateCommandHandler EmployeeCreateCommandHandlerUnderTest { get; }
        protected Mock<IEmployeeRepository> EmployeeRepositoryMock { get; }
        protected Mock<IUnitOfWork> UnitOfWorkMock { get; }

        public EmployeeCreateCommandHandlerTest()
        {
            EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            EmployeeCreateCommandHandlerUnderTest = new EmployeeCreateCommandHandler(EmployeeRepositoryMock.Object, UnitOfWorkMock.Object);
        }

        public class Handle : EmployeeCreateCommandHandlerTest
        {
            [Fact]
            public void should_handle_create_employee_command()
            {
                // Arrange 
                var inputCommand = 
                    new EmployeeCreateCommand("joe", "bedford", "joe@email.com");

                EmployeeRepositoryMock
                    .Setup(i => i.CreateAsync(It.IsAny<Employee>()))
                    .Returns(Task.CompletedTask);

                UnitOfWorkMock
                    .Setup(i => i.CommitAsync(default(CancellationToken)))
                    .ReturnsAsync(1);

                // Act
                var result = EmployeeCreateCommandHandlerUnderTest.Handle(inputCommand, default(CancellationToken)).Result;

                // Assert
                Assert.NotEqual(Guid.Empty, result.Id);
            }
        }
    }
}
