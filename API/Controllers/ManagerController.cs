using API.DTOs.LeaveType;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using API.DTOs.LeaveRequest;
using API.DTOs.Manager;

namespace API.Controllers;

[ApiController]
[Route("api/manager")]
public class ManagerController : ControllerBase
{
    private readonly ManagerService _service;

    public ManagerController(ManagerService service)
    {
        _service = service;
    }

    [HttpGet("employees/{managerGuid}/leave-requests")]
    public IActionResult GetLeaveRequests(Guid managerGuid)
    {
        var leaveRequests = _service.GetLeaveRequestsByManagerGuid(managerGuid);

        if (!leaveRequests.Any())
        {
            return NotFound(new ResponseHandler<IEnumerable<GetEmployeeRequestDto>>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No leave requests found for the manager's subordinates."
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GetEmployeeRequestDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Leave requests found for the manager's subordinates.",
            Data = leaveRequests
        });
    }

    [HttpPut("leave-requests/status")]
    public IActionResult UpdateLeaveRequestStatus(UpdateStatusRequestDto updateStatusDto)
    {
        bool isUpdateSuccessful = _service.UpdateLeaveRequestStatus(updateStatusDto);

        if (!isUpdateSuccessful)
        {
            return NotFound(new ResponseHandler<UpdateStatusRequestDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "LeaveRequest not found or failed to update status.",
            });
        }

        return Ok(new ResponseHandler<string>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Leave request status updated successfully.",
            Data = "Successfully updated"
        });

    }



}
