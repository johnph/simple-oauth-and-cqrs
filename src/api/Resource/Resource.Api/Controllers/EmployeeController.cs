namespace Resource.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Resource.Infrastructure.Commands;
    using Resource.Infrastructure.Queries;
    using System;
    using System.Threading.Tasks;

    [Authorize(Policy = "ApiExecute")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeRequest request)
        {
            var employee = await _mediator.Send(new EmployeeCreateCommand(request.Name, request.Address, request.Email));
            return Created(string.Empty, employee);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> Read([FromRoute] Guid employeeId)
        {
            var orderDetails = await _mediator.Send(new EmployeeReadQuery(employeeId));
            return Ok(orderDetails);
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> Update([FromRoute]Guid employeeId, [FromBody] EmployeeRequest request)
        {
            var employee = await _mediator.Send(new EmployeeUpdateCommand(employeeId, request.Name, request.Address, request.Email));
            return Ok();
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid employeeId)
        {
            var employee = await _mediator.Send(new EmployeeDeleteCommand(employeeId));
            return Ok();
        }
    }
}