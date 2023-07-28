using Microsoft.AspNetCore.Mvc;
using Client.ViewModels.Account;
using Client.Contract;
using API.DTOs.Accounts;
using NuGet.Protocol.Core.Types;

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
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        else if (result.Status == "OK")
        {
            HttpContext.Session.SetString("JWToken", result.Data);
            return RedirectToAction("Index", "Employee");
        }
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Account");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

}
