using API.DTOs.LeaveRequest;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("Api/LeaveRequests")]
public class LeaveRequestController : ControllerBase
{
    private readonly LeaveRequestService _service;

    public LeaveRequestController(LeaveRequestService service)
    {
    _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var entities = _service.GetLeaveRequest();
        if (entities == null)
        {
            return NotFound(new ResponseHandler<GetLeaveRequestDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }
        return Ok(new ResponseHandler<IEnumerable<GetLeaveRequestDto>>
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
        var leaveRequest = _service.GetLeaveRequest(guid);
        if (leaveRequest is null)
        {
            return NotFound(new ResponseHandler<GetLeaveRequestDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<GetLeaveRequestDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = leaveRequest
        });
    }

    [HttpPost]
    public IActionResult Create(NewLeaveRequestDto newLeaveRequestDto)
    {
        var createLeaveRequest = _service.CreateLeaveRequest(newLeaveRequestDto);
        if (createLeaveRequest is null)
        {
            return BadRequest(new ResponseHandler<GetLeaveRequestDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });
        }

        return Ok(new ResponseHandler<GetLeaveRequestDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully created",
            Data = createLeaveRequest
        });
    }

    [HttpPut]
    public IActionResult Update(UpdateLeaveRequestDto updateLeaveRequestDto)
    {
        var update = _service.UpdateLeaveRequest(updateLeaveRequestDto);
        if (update is -1)
        {
            return NotFound(new ResponseHandler<UpdateLeaveRequestDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }
        if (update is 0)
        {
            return BadRequest(new ResponseHandler<UpdateLeaveRequestDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check your data"
            });
        }
        return Ok(new ResponseHandler<UpdateLeaveRequestDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully updated"
        });

    }

    [HttpDelete]
    public IActionResult Delete(Guid guid) 
    { 
        var delete = _service.DeleteLeaveRequest(guid);

        if (delete is -1)
        {
            return NotFound(new ResponseHandler<GetLeaveRequestDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }
        if (delete is 0)
        {
            return BadRequest(new ResponseHandler<GetLeaveRequestDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check connection to database"
            });
        }

        return Ok(new ResponseHandler<GetLeaveRequestDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted"
        });
    }
}


