using API.DTOs.Employees;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _service.GetEmployee();

            if (entities == null)
            {
                return NotFound(new ResponseHandler<GetEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetEmployeeDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data found",
                Data = entities
            });
        }
    }
}
