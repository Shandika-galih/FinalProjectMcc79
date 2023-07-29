﻿using Client.Repositories;
using API.Utilities.Handler;
using Client.ViewModels.Account;
using API.DTOs.Accounts;
using API.Utilities;
using API.Models;

namespace Client.Contract;

public interface IAccountRepository : IGeneralRepository<Account, string>
{
    Task<ResponseHandler<string>> Login(LoginVM loginVM);
    Task<ResponseHandler<string>> ForgotPassword(ForgotPasswordVM forgotPasswordVM);
    Task<ResponseHandler<string>> ChangePassword(ChangePasswordVM changePasswordVM);

}