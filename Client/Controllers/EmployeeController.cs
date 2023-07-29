﻿using API.DTOs.Employees;
using API.Models;
using Client.Contract;
using Client.ViewModels.Account;
using Client.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;

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

    [HttpGet]
    public async Task<IActionResult> Edit(Guid guid)
    {
        var result = await repository.Get(guid);

        if (result.Data?.Guid is null)
        {
            return RedirectToAction(nameof(Index));
        }

        var employee = new EmployeeVM
        {
            Guid = result.Data.Guid,
            NIK = result.Data.NIK,
            FirstName = result.Data.FirstName,
            LastName = result.Data.LastName,
            Gender = result.Data.Gender,
            PhoneNumber = result.Data.PhoneNumber,
            EligibleLeave = result.Data.EligibleLeave,
            HiringDate = result.Data.HiringDate,
            ManagerGuid = result.Data.ManagerGuid,
            Email = result.Data.Email,
            Password = result.Data.Password,
            ConfirmPassword = result.Data.ConfirmPassword
        };

        var resultManager = await _managerRepository.Get();
        var listManagers = new List<ManagerVM>();

        if (resultManager.Data != null)
        {
            listManagers = resultManager.Data.ToList();
        }

        // add to view data
        ViewData["Managers"] = listManagers;

        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EmployeeVM employee)
    {
        var result = await repository.Put(employee.Guid, employee);
        if (result.Code == 200)
        {
            return RedirectToAction(nameof(Index));
        }
        else if (result.Status == "409")
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return View();
    }
}