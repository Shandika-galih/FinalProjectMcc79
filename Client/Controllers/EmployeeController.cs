using API.DTOs.Employees;
using API.Models;
using Client.Contract;
using Client.ViewModels.Employee;
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
        var ListEmployee = new List<EmployeeVM>();

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
    public async Task<IActionResult> Create(EmployeeVM newEmploye)
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

    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        try
        {
            var result = await repository.Delete(guid);

            if (result.Status == "200" && result.Data?.Guid != null)
            {
                // Tangani situasi ketika penghapusan berhasil
                // Misalnya, menyiapkan data untuk ditampilkan di tampilan terkait
                var employee = new Employee
                {
                    Guid = result.Data.Guid
                };

                TempData["Success"] = "Data berhasil dihapus";
            }
            else
            {
                TempData["Error"] = "Gagal menghapus data";
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Terjadi kesalahan saat menghapus data: " + ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

   /* [HttpGet]
    public async Task<IActionResult> Edit(Guid guid)
    {
        var result = await repository.Get(guid);

        if (result.Data?.Guid is null)
        {
            return RedirectToAction(nameof(Index));
        }

        var employee = new GetEmployeeDto
        {
            Guid = result.Data.Guid,
            Nik = result.Data.Nik,
            FirstName = result.Data.FirstName,
            LastName = result.Data.LastName,
            Birtdate = result.Data.Birtdate,
            Gender = result.Data.Gender,
            HiringDate = result.Data.HiringDate,
            Email = result.Data.Email,
            PhoneNumber = result.Data.PhoneNumber
        };

        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(GetEmployeeDto employee)
    {
        if (!ModelState.IsValid)
        {
            return View(employee);
        }
        var result = await repository.Put(employee.Guid, employee);

        if (result.Status == "200")
        {
            TempData["Success"] = "Data berhasil diubah";
        }
        else
        {
            TempData["Error"] = "Gagal menghapus data";
        }
        return RedirectToAction(nameof(Index));
    }*/
}