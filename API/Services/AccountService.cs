using API.Contracts;
using API.DTOs.Accounts;
using API.Models;
using API.Repositories;
using API.Utilities;
using API.Utilities.Handler;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace API.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ITokenHandler _tokenHandler;
    private readonly IEmailHandler _emailHandler;

    public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IAccountRoleRepository accountRoleRepository, IRoleRepository roleRepository, ITokenHandler tokenHandler, IEmailHandler emailHandler)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _accountRoleRepository = accountRoleRepository;
        _roleRepository = roleRepository;
        _tokenHandler = tokenHandler;
        _emailHandler = emailHandler;
    }

    public IEnumerable<GetAccountDto>? GetAccount()
    {
        var accounts = _accountRepository.GetAll();
        if (!accounts.Any())
        {
            return null; // No Account  found
        }

        var toDto = accounts.Select(account =>
                                            new GetAccountDto
                                            {
                                                Guid = account.Guid,
                                                Email = account.Email,
                                                Password = account.Password
                                            }).ToList();

        return toDto; // Account found
    }

    public GetAccountDto? GetAccount(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null)
        {
            return null; // account not found
        }

        var toDto = new GetAccountDto
        {
            Guid = account.Guid
        };

        return toDto; // accounts found
    }

    public GetAccountDto? CreateAccount(NewAccountDto newAccountDto)
    {
        var account = new Account
        {
            Guid = newAccountDto.Guid,
            Email = newAccountDto.Email,
            Password = Hashing.HashPassword(newAccountDto.Password),
        };

        var createdAccount = _accountRepository.Create(account);
        if (createdAccount is null)
        {
            return null; // Account not created
        }

        var toDto = new GetAccountDto
        {
            Guid = createdAccount.Guid,
            Email = createdAccount.Email,
            Password = createdAccount.Password
        };

        return toDto; // Account created
    }

    public int UpdateAccount(UpdateAccountDto updateAccountDto)
    {
        var isExist = _accountRepository.IsExist(updateAccountDto.Guid);
        if (!isExist)
        {
            return -1; // Account not found
        }

        var getAccount = _accountRepository.GetByGuid(updateAccountDto.Guid);

        var account = new Account
        {
            Guid = updateAccountDto.Guid,
            Email = updateAccountDto.Email,
            Password = Hashing.HashPassword(updateAccountDto.Password)
        };

        var isUpdate = _accountRepository.Update(account);
        if (!isUpdate)
        {
            return 0; // Account not updated
        }

        return 1;
    }

    public int DeleteAccount(Guid guid)
    {
        var isExist = _accountRepository.IsExist(guid);
        if (!isExist)
        {
            return -1; // Account not found
        }

        var account = _accountRepository.GetByGuid(guid);
        var isDelete = _accountRepository.Delete(account!);
        if (!isDelete)
        {
            return 0; // Account not deleted
        }
        return 1;
    }

    public string Login(LoginDto login)
    {
        var emailEmp = _accountRepository.GetEmail(login.Email);
        if (emailEmp is null)
        {
            return "0";
        }

        //var password = _accountRepository.GetByGuid(emailEmp.Guid);
        var isValid = Hashing.ValidatePassword(login.Password, emailEmp!.Password);
        if (!isValid)
        {
            return "-1";
        }
        var employee = _employeeRepository.GetByGuid(emailEmp.Guid);

        try
        {
            var claims = new List<Claim>() {
                new Claim("NIK", employee.NIK.ToString()),
                new Claim("FullName", $"{ employee.FirstName} {employee.LastName}"),
                new Claim("Email", login.Email)
            };

            var getAccountRole = _accountRoleRepository.GetAccountRolesByAccountGuid(employee.Guid);
            var getRoleNameByAccountRole = from ar in getAccountRole
                                           join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                                           select r.Name;

            claims.AddRange(getRoleNameByAccountRole.Select(role => new Claim(ClaimTypes.Role, role)));

            var getToken = _tokenHandler.GenerateToken(claims);
            return getToken;
        }
        catch
        {
            return "-2";
        }
    }

    /*public int ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var account = _accountRepository.GetEmail(changePasswordDto.Email);
        if (account is null)
            return 0; // Email not found

        var accountGuid = _accountRepository.GetByGuid(account.Guid);
        if (accountGuid is null)
            return 0; // Email not found


        var isUpdated = _accountRepository.Update(new Account
        {
            Guid = account.Guid,
            Password = Hashing.HashPassword(changePasswordDto.NewPassword),
      
        });

        return isUpdated ? 1    // Success
                         : -4;  // Database Error
    }*/

    public int ForgotPassword(ForgotPasswordDto forgotPassword)
    {
        var entity = _accountRepository.GetEmail(forgotPassword.Email);
        if (entity is null)
            return 0; // Email not found

        var account = _accountRepository.GetByGuid(entity.Guid);
        if (account is null)
            return -1;

        var otp = new Random().Next(111111, 999999);
        var isUpdated = _accountRepository.Update(new Account
        {
            Guid = account.Guid,
            Email = account.Email,
            Password = account.Password,
            OTP = otp,
            ExpiredTime = DateTime.Now.AddMinutes(5),
            IsUsed = false,
 
        });

        if (!isUpdated)
        {
            return -1;
        }

        _emailHandler.SendEmail(forgotPassword.Email,
                                "Forgot Password",
                                $"Your OTP is {otp}");

        return 1;
    }

    public int ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var isExist = _accountRepository.GetEmail(changePasswordDto.Email);
        if (isExist is null)
        {
            return -1; // Account not found
        }

        var getAccount = _accountRepository.GetByGuid(isExist.Guid);
        if (getAccount.OTP != changePasswordDto.Otp)
        {
            return 0;
        }

        if (getAccount.IsUsed == true)
        {
            return 1;
        }

        if (getAccount.ExpiredTime < DateTime.Now)
            return -3; // OTP is expired


        var account = new Account
        {
            Guid = getAccount.Guid,
            Email = getAccount.Email,
            Password = Hashing.HashPassword(changePasswordDto.NewPassword),
            OTP = getAccount.OTP,
            IsUsed = true,
        };

        var isUpdate = _accountRepository.Update(account);
        if (!isUpdate)
        {
            return 0; // Account not updated
        }

        return 3;
    }



}
