using API.DTOs.Accounts;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;

    public AccountController(AccountService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var entities = _service.GetAccount();

        if (entities == null)
        {
            return NotFound(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GetAccountDto>>
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
        var account = _service.GetAccount(guid);
        if (account is null)
        {
            return NotFound(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<GetAccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = account
        });
    }

    [HttpPost]
    public IActionResult Create(NewAccountDto newAccountDto)
    {
        var createAccount = _service.CreateAccount(newAccountDto);
        if (createAccount is null)
        {
            return BadRequest(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });
        }

        return Ok(new ResponseHandler<GetAccountDto>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Successfully created",
            Data = createAccount
        });
    }

    [HttpPut]
    public IActionResult Update(UpdateAccountDto updateAccountDto)
    {
        var update = _service.UpdateAccount(updateAccountDto);
        if (update is -1)
        {
            return NotFound(new ResponseHandler<UpdateAccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }
        if (update is 0)
        {
            return BadRequest(new ResponseHandler<UpdateAccountDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Check your data"
            });
        }
        return Ok(new ResponseHandler<UpdateAccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully updated"
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var delete = _service.DeleteAccount(guid);

        if (delete is -1)
        {
            return NotFound(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }
        if (delete is 0)
        {
            return BadRequest(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check connection to database"
            });
        }

        return Ok(new ResponseHandler<GetAccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted"
        });
    }

    // to do: login 

    [HttpPost("login")]
    public IActionResult LoginRequest(LoginDto login)
    {
        var entities = _service.Login(login);
        if (entities is "-1")
        {
            return NotFound(new ResponseHandler<LoginDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Incorrect Email / Password"
            });
        }

        if (entities is "0")
        {
            return BadRequest(new ResponseHandler<LoginDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Incorrect Email / Password"
            });
        }

        if (entities is "-2")
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<LoginDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error to database"
            });
        }

        return Ok(new ResponseHandler<string>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Login success",
            Data = entities
        });
    }

    /*[HttpPost("ChangePassword")]
    public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var isUpdated = _service.ChangePassword(changePasswordDto);
        if (isUpdated == 0)
            return NotFound(new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Email not found"
            });

        if (isUpdated is -4)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<GetAccountDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving data from the database"
            });

        return Ok(new ResponseHandler<GetAccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Password has been changed successfully"
        });
    }*/

    [HttpPost("ForgotPassword")]
    public IActionResult ForgotPassword(ForgotPasswordDto forgotPassword)
    {
        var isUpdated = _service.ForgotPassword(forgotPassword);
        if (isUpdated == 0)
            return NotFound(new ResponseHandler<NewAccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Email not found"
            });

        if (isUpdated is -1)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<NewAccountDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving data from the database"
            });

        return Ok(new ResponseHandler<NewAccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Otp has been sent to your email"
        });
    }

    [HttpPut("changePassword")]
    public IActionResult Update(ChangePasswordDto changePasswordDto)
    {
        var update = _service.ChangePassword(changePasswordDto);
        if (update is -1)
        {
            return NotFound(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Email not Found"
            });
        }
        if (update is 0)
        {
            return NotFound(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Otp doesn't match"
            });
        }
        if (update is 1)
        {
            return NotFound(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Otp has been used"
            });
        }
        if (update is 2)
        {
            return NotFound(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Otp alredy expired"
            });
        }
        return Ok(new ResponseHandler<ChangePasswordDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully updated"
        });
    }


}
