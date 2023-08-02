using API.DTOs.LeaveType;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using API.DTOs.LeaveRequest;
using API.DTOs.Manager;
using API.DTOs.Employees;
using API.Contracts;

namespace API.Controllers;

[ApiController]
[Route("api/manager")]
public class ManagerController : ControllerBase
{
    private readonly ManagerService _service;
    private readonly EmployeeService _employeeService;
    private readonly ITokenHandler _tokenHandler;

    public ManagerController(ManagerService service, EmployeeService employeeService, ITokenHandler tokenHandler)
    {
        _service = service;
        _employeeService = employeeService;
        _tokenHandler = tokenHandler;
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

    [HttpGet("leave-requests")]
    public IActionResult GetLeaveRequests()
    {
        try
        {
            string token = _tokenHandler.GetTokenFromHeader(Request);

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("JWT token not found in the request.");
            }

            var jwtPayload = _tokenHandler.DecodeJwtToken(token);

            if (jwtPayload == null || !jwtPayload.ContainsKey("Guid"))
            {
                throw new Exception("NIK not found in the JWT token.");
            }

            string guidString = jwtPayload["Guid"].ToString();
            if (!Guid.TryParse(guidString, out Guid guid))
            {
                throw new Exception("Invalid Guid format.");
            }

            var history = _service.GetLeaveRequestsByManagerGuid(guid);

            if (history == null || !history.Any())
            {
                return NotFound(new ResponseHandler<GetEmployeeRequestDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetEmployeeRequestDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data found",
                Data = history
            });
        }
        catch (Exception ex)
        {
            // Exception occurred, handle and return "Data not found" response
            return NotFound(new ResponseHandler<GetEmployeeRequestDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

    }

   /* [HttpPut("leave-requests/status")]
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

    }*/

}
