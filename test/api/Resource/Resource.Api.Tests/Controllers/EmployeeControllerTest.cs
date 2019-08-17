using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Resource.Api.Controllers;
using Resource.Infrastructure.Commands;
using Resource.Infrastructure.Queries;
using System;
using System.Threading;
using Xunit;

namespace Resource.Api.Tests.Controllers
{
    public class EmployeeControllerTest
    {
        protected EmployeeController EmployeeControllerUnderTest { get; }
        protected Mock<IMediator> MediatorMock { get; }

        public EmployeeControllerTest()
        {
            MediatorMock = new Mock<IMediator>();
            EmployeeControllerUnderTest = new EmployeeController(MediatorMock.Object);
        }

        public class Create : EmployeeControllerTest
        {
            [Fact]
            public void should_receive_request_to_create_employee()
            {
                // Arrange
                var inputRequest = new EmployeeRequest()
                {
                    Name = "joe",
                    Address = "wellington",
                    Email = "joe@email.com"
                };

                MediatorMock
                    .Setup(i => i.Send(It.IsAny<EmployeeCreateCommand>(), default(CancellationToken)))
                    .ReturnsAsync(employeeCommand);

                // Act
                var result = EmployeeControllerUnderTest.Create(inputRequest).Result;

                // Assert
                Assert.IsType<CreatedResult>(result);
            }

            private EmployeeCommand employeeCommand => new EmployeeCommand()
            {
                Id = Guid.NewGuid()
            };
        }

        public class Read : EmployeeControllerTest
        {
            [Fact]
            public void should_receive_request_and_send_response()
            {
                // Arrange
                MediatorMock
                   .Setup(i => i.Send(It.IsAny<EmployeeReadQuery>(), default(CancellationToken)))
                   .ReturnsAsync(AnyEmployeeResponse);

                // Act
                var result = EmployeeControllerUnderTest.Read(AnyEmployeeResponse.EmployeeId).Result;

                // Assert
                Assert.IsType<OkObjectResult>(result);
            }
        }

        public class Update : EmployeeControllerTest
        {
            [Fact]
            public void should_receive_request_and_update_employee()
            {
                // Arrange
                EmployeeRequest inputRequest = new EmployeeRequest()
                {
                    Name = "joe",
                    Address = "waterloo",
                    Email = "joe@email.com"
                };

                MediatorMock
                    .Setup(i => i.Send(It.IsAny<EmployeeUpdateCommand>(), default(CancellationToken)))
                    .ReturnsAsync(Unit.Value);

                // Act
                var result = EmployeeControllerUnderTest.Update(AnyEmployeeResponse.EmployeeId, inputRequest).Result;

                // Assert
                Assert.IsType<OkResult>(result);
            }
        }

        public class Delete : EmployeeControllerTest
        {
            [Fact]
            public void should_receive_request_and_delete_request()
            {
                // Arrange
                MediatorMock
                    .Setup(i => i.Send(It.IsAny<EmployeeDeleteCommand>(), default(CancellationToken)))
                    .ReturnsAsync(Unit.Value);

                // Act
                var result = EmployeeControllerUnderTest.Delete(AnyEmployeeResponse.EmployeeId).Result;

                // Assert
                Assert.IsType<OkResult>(result);
            }
        }

        public EmployeeResponse AnyEmployeeResponse => new EmployeeResponse()
        {
            EmployeeId = Guid.NewGuid(),
            Name = "joe",
            Address = "waterloo",
            Email = "joe@email.com"
        };
    }
}
