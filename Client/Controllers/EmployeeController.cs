using API.DTOs.Employees;
using API.Models;
using Client.Contract;
using Client.ViewModels.Account;
using Client.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository repository;
    private readonly IRoleRepository _roleRepository;
    private readonly IManagerRepository _managerRepository;


    public EmployeeController(IEmployeeRepository repository, IRoleRepository roleRepository, IManagerRepository managerRepository)
    {
        this.repository = repository;
        _roleRepository = roleRepository;
        _managerRepository = managerRepository;
    }

    public async Task<IActionResult> Index()
    {
        var result = await repository.GetEmployees();
        var listEmployee = new List<EmployeeVM>();

        if (result.Data != null)
        {
            listEmployee = result.Data.ToList();
        }

        return View(listEmployee);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var result = await _managerRepository.Get();
        var listManagers = new List<ManagerVM>();

        if (result.Data != null)
        {
            listManagers = result.Data.ToList();
        }

        // add to view data
        ViewData["Managers"] = listManagers;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeVM newEmploye)
    {

        var result = await repository.Post(newEmploye);
        if (result.Code == 201)
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