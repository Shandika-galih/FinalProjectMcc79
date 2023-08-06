using Microsoft.AspNetCore.Mvc;
using Client.ViewModels.Account;
using Client.Contract;
using API.DTOs.Accounts;
using NuGet.Protocol.Core.Types;
using Microsoft.AspNetCore.Authorization;

namespace Client.Controllers;

public class AccountController : Controller
{
    private readonly IAccountRepository repository;

    public AccountController(IAccountRepository accountRepository)
    {
        this.repository = accountRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        var result = await repository.Login(loginVM);
        if (result is null)
        {
            return RedirectToAction("Error", "Home");
        }
        else if (result.Status == "BadRequest")
        {
            TempData["Error"] = result.Message;
            return Redirect("~/Account/Login");
        }
        else if (result.Status == "NotFound")
        {
            TempData["Error"] = result.Message;
            return Redirect("~/Account/Login");
        }
        else if (result.Status == "OK")
        {
            HttpContext.Session.SetString("JWToken", result.Data);
            return RedirectToAction("Index", "Home");
        }
        return View();
    }


    [HttpGet]
    public IActionResult ForgotPass()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPass(ForgotPasswordVM forgotPasswordVM)
    {
        var result = await repository.ForgotPassword(forgotPasswordVM);
        if (result == null)
        {
            return RedirectToAction("Error", "Index");
        }
        else if (result.Code == 404)
        {
            ModelState.AddModelError(string.Empty, result.Message); 
            return View();
        }
        else if (result.Status == "OK")
        {
            TempData["ForgotPasswordEmail"] = forgotPasswordVM.Email;
            return RedirectToAction("ChangePass", "Account");
        }
        return View();
    }

    [HttpGet]
    public IActionResult ChangePass()
    {
        var email = TempData["ForgotPasswordEmail"] as string; 
        if (string.IsNullOrEmpty(email))
        {
            return RedirectToAction(nameof(ForgotPass));
        }

        var changePass = new ChangePasswordVM
        {
            Email = email, 
        };

        return View(changePass);
    }

    [HttpPost]
    public async Task<IActionResult> ChangePass(ChangePasswordVM changePasswordVM)
    {
        var result = await repository.ChangePassword(changePasswordVM);
        if (result == null)
        {
            return RedirectToAction("Error", "Index");
        }
        else if (result.Code == 404)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        else if (result.Status == "OK")
        {
            return RedirectToAction("Login", "Account");
        }
        return View();
    }

    [Authorize]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }

}
