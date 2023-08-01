using API.DTOs.LeaveType;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using API.DTOs.LeaveRequest;
using API.DTOs.Manager;
using API.DTOs.Employees;

namespace API.Controllers;

[ApiController]
[Route("api/manager")]
public class ManagerController : ControllerBase
{
    private readonly ManagerService _service;
    private readonly EmployeeService _employeeService;

    public ManagerController(ManagerService service, EmployeeService employeeService)
    {
        _service = service;
        _employeeService = employeeService;
    }

    [HttpGet]
    public IActionResult GetManagers()
    {
        var entities = _employeeService.GetManagers();

        if (entities == null)
        {
            return NotFound(new ResponseHandler<GetDataEmployeeDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GetDataEmployeeDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = entities
        });
    }

    [HttpGet("leave-requests/{managerGuid}")]
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
