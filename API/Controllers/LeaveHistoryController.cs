using API.DTOs.LeaveHistory;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("Api/leave_history")]
public class LeaveHistoryController : ControllerBase

{
    private readonly LeaveHistoryService _service;

    public LeaveHistoryController(LeaveHistoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var entities = _service.GetLeaveHistory();

        if (entities == null)
        {
            return NotFound(new ResponseHandler<GetLeaveHistoryDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GetLeaveHistoryDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = entities
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var entities = _service.getLeaveHistroy(guid);
        if (entities is null)
        {
            return NotFound(new ResponseHandler<GetLeaveHistoryDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<GetLeaveHistoryDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = entities
        });
    }

    [HttpPost]
    public IActionResult Create(NewLeaveHistoryDto newLeaveHistoryDto)
    {
        var createLeaveHistory = _service.CreateLeaveHistory(newLeaveHistoryDto);
        if (createLeaveHistory is null)
        {
            return BadRequest(new ResponseHandler<GetLeaveHistoryDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });
        }

        return Ok(new ResponseHandler<GetLeaveHistoryDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully created",
            Data = createLeaveHistory
        });
    }

    [HttpPut]
    public IActionResult Update(UpdateLeaveHistoryDto updateLeaveHistoryDto)
    {
        var update = _service.UpdateLeaveHistory(updateLeaveHistoryDto);
        if (update is -1)
        {
            return NotFound(new ResponseHandler<UpdateLeaveHistoryDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }
        if (update is 0)
        {
            return BadRequest(new ResponseHandler<UpdateLeaveHistoryDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check your data"
            });
        }
        return Ok(new ResponseHandler<UpdateLeaveHistoryDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully updated"
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var delete = _service.DeleteleaveHistory(guid);

        if (delete is -1)
        {
            return NotFound(new ResponseHandler<GetLeaveHistoryDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }
        if (delete is 0)
        {
            return BadRequest(new ResponseHandler<GetLeaveHistoryDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check connection to database"
            });
        }

        return Ok(new ResponseHandler<GetLeaveHistoryDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted"
        });
    }

    [HttpGet("GetAllLeaveHistory")]
    public IActionResult GetAllEmployee()
    {
        var entities = _service.GetLeaveHistroyEmployees();
        if (entities == null)
        {
            return NotFound(new ResponseHandler<GetLeaveHistroyEmployeeDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }
        return Ok(new ResponseHandler<IEnumerable<GetLeaveHistroyEmployeeDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = entities
        });
    }

    [HttpGet("history/{guid_employee}")]
    public IActionResult GetByGuidEmployee(Guid guid_employee)
    {
        var employees = _service.GetLeaveHistroyEmployee(guid_employee); 

        if (employees == null || !employees.Any()) 
        {
            return NotFound(new ResponseHandler<GetLeaveHistroyEmployeeDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GetLeaveHistroyEmployeeDto>> 
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = employees 
        });
    }


}
