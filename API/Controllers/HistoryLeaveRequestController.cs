using API.Contracts;
using API.DTOs.LeaveRequest;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("Api/HistoryLeaveRequest")]
public class HistoryLeaveRequestController : Controller
{
    private readonly HistoryLeaveRequestService _historyService;
    private readonly ITokenHandler _tokenHandler;

    public HistoryLeaveRequestController(HistoryLeaveRequestService requestService, ITokenHandler tokenHandler)
    {
        _historyService = requestService;
        _tokenHandler = tokenHandler;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var entities = _historyService.GetHistoryEmployeeRequest();

        if (entities == null)
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
            Data = entities
        });
    }

    [HttpGet("AllHistoryApprove")]
    public IActionResult GetAllHistoryApprove()
    {
        var entities = _historyService.GetHistoryApprove();

        if (entities == null)
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
            Data = entities
        });
    }

    [HttpGet("AllHistoryPending")]
    public IActionResult GetAllHistoryPending()
    {
        var entities = _historyService.GetHistoryPending();

        if (entities == null)
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
            Data = entities
        });
    }

    [HttpGet("AllHistoryReject")]
    public IActionResult GetAllHistoryReject()
    {
        var entities = _historyService.GetHistoryReject();

        if (entities == null)
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
            Data = entities
        });
    }

    [HttpGet("byNik")]
    public IActionResult GetByGuidEmployee()
    {
        try
        {
            string token = _tokenHandler.GetTokenFromHeader(Request);

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("JWT token not found in the request.");
            }

            var jwtPayload = _tokenHandler.DecodeJwtToken(token);

            if (jwtPayload == null || !jwtPayload.ContainsKey("NIK"))
            {
                throw new Exception("NIK not found in the JWT token.");
            }

            var nik = Convert.ToInt32(jwtPayload["NIK"]);
            var history = _historyService.GetHistorybyNik(nik);

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

    [HttpGet("byManager")]
    public IActionResult GetByGuidManager()
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
                throw new Exception("Guid not found in the JWT token.");
            }

            string guidString = jwtPayload["Guid"].ToString();
            if (!Guid.TryParse(guidString, out Guid guid))
            {
                throw new Exception("Invalid Guid format.");
            }

            var history = _historyService.GetByGuidManager(guid);

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
}