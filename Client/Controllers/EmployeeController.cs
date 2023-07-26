using Client.Contract;
using API.DTOs.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository repository;

    public EmployeeController(IEmployeeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var result = await repository.Get();
        var ListEmployee = new List<GetDataEmployeeDto>();

        if (result.Data != null)
        {
            ListEmployee = result.Data.ToList();
        }
        return View(ListEmployee);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(GetDataEmployeeDto newEmploye)
    {

        var result = await repository.Post(newEmploye);
        if (result.Status == "200")
        {
            TempData["Success"] = "Data berhasil masuk";
            return RedirectToAction(nameof(Index));
        }
        else if (result.Status == "409")
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        return RedirectToAction(nameof(Index));

    }

}