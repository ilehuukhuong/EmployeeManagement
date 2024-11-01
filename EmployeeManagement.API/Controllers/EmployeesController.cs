using EmployeeManagement.Application.Employees;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    public class EmployeesController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrUpdateDto createEmployee)
        {
            return HandleCreatedResult(await Mediator.Send(new Create.Command { CreateDto = createEmployee }), nameof(GetEmployeeById));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateOrUpdateDto updateEmployee)
        {
            updateEmployee.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { UdpadeDto = updateEmployee }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
        }
    }
}
