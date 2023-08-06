using API.DTOs.Employees;
using API.Models;
using Client.Contract;
using Client.ViewModels.LeaveType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class LeaveTypeController : Controller
{
    private readonly ILeaveTypeRepository repository;

    public LeaveTypeController(ILeaveTypeRepository repository)
    {
        this.repository = repository;
    }
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var result = await repository.Get();
        var ListLeaveType = new List<LeaveTypeVM>();

        if (result.Data != null)
        {
            ListLeaveType = result.Data.ToList();
        }
        return View(ListLeaveType);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult AddType()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddType(LeaveTypeVM leaveTypeVM)
    {
        var result = await repository.Post(leaveTypeVM);
        if (result.Status == "200")
        {
            TempData["Success"] = "Data berhasil masuk";
            return RedirectToAction(nameof(Index));
        }
        else if (result.Status == "404")
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View(leaveTypeVM); // Kembalikan tampilan View dengan data yang sudah diisi sebelumnya
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
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> EditType(Guid guid)
    {
        var result = await repository.Get(guid);

        if (result.Data?.Guid is null)
        {
            return RedirectToAction(nameof(Index));
        }

        var leaveType = new LeaveTypeVM
        {
            Guid = result.Data.Guid,
            LeaveName = result.Data.LeaveName,
            LeaveDay = result.Data.LeaveDay,
            LeaveDescription = result.Data.LeaveDescription,

        };

        return View(leaveType);
    }

    [HttpPost]
    public async Task<IActionResult> EditType(LeaveTypeVM leaveType)
    {
        if (!ModelState.IsValid)
        {
            return View(leaveType);
        }
        var result = await repository.Put(leaveType.Guid, leaveType);

        if (result.Status == "200")
        {
            TempData["Success"] = "Data berhasil diubah";
        }
        else
        {
            TempData["Error"] = "Gagal menghapus data";
        }
        return RedirectToAction(nameof(Index));
    }
}