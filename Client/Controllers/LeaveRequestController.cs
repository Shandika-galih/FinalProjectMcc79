using API.Models;
using Client.Contract;
using Client.Repositories;
using Client.Utilities;
using Client.ViewModels.Employee;
using Client.ViewModels.LeaveHistory;
using Client.ViewModels.LeaveRequest;
using Client.ViewModels.LeaveType;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Client.Controllers;

public class LeaveRequestController : Controller
{
    private readonly ILeaveRequestRepository _repository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IManagerRepository _managerRepository;

    public LeaveRequestController(ILeaveRequestRepository repository, IEmployeeRepository employeeRepository, ILeaveTypeRepository leaveType, IManagerRepository managerRepository)
    {
        _repository = repository;
        _employeeRepository = employeeRepository;
        _leaveTypeRepository = leaveType;
        _managerRepository = managerRepository;

    }

    public async Task<IActionResult> Index()
    {
        var result = await _repository.Get();
        var listRequest = new List<LeaveRequestVM>();

        if (result.Data != null)
        {
            listRequest = result.Data.ToList();
        }

        return View(listRequest);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var guid = User.Claims.FirstOrDefault(x => x.Type == "Guid")?.Value;
        var guidTemp = Guid.Parse(guid);
        var employee = await _employeeRepository.Get(guidTemp);
        var manager = (await _managerRepository.Get()).Data?.ToList();
        var isManager = manager?.FirstOrDefault(m => m.Guid == employee.Data.ManagerGuid) ?? new ManagerVM();

        if (isManager.Guid != null)
        {
            ViewData["Manager"] = isManager;
        }
        else
        {
            ViewData["Manager"] = false;
        }

        var resultLeaveType = await _leaveTypeRepository.Get();
        var listLeaveTypes = new List<LeaveTypeVM>();

        if (resultLeaveType.Data != null)
        {
            listLeaveTypes = resultLeaveType.Data.ToList();

        }

        // add to view data
        ViewData["Employee"] = employee.Data;
        ViewData["LeaveTypes"] = listLeaveTypes;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(LeaveRequestVM leaveRequest)
    {
        string tmpAttachment = leaveRequest.attachmentBase64;
        leaveRequest.Attachment = tmpAttachment;
        var result = await _repository.Post(leaveRequest);
        if (result.Code == 200)
        {
            return RedirectToAction("Index", "Employee");
        }
        else if (result.Status == "409")
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetByManager()
    {
        var guid = User.Claims.FirstOrDefault(a => a.Type == "Guid")?.Value;
        var guidTemp = Guid.Parse(guid);
        var result = await _repository.GetByManager(guidTemp);
        var requests = new List<LeaveRequestVM>();

        if (result.Data is not null)
        {
            requests = result.Data.ToList();
        }
        return View(requests);
    }

    [HttpGet]
    public async Task<IActionResult> ApproveStatuss(Guid guid)
    {
        var result = await _repository.Get(guid);

        if (result.Data?.Guid is null)
        {
            return RedirectToAction(nameof(Index));
        }
        var leaveRequest = new UpdateStatusRequestVM
        {
            Guid = result.Data.Guid,
            FullName = result.Data.FullName,
            NIK = result.Data.NIK,
            Remarks = result.Data.Remarks,
            SubmitDate = result.Data.SubmitDate,
            StartDate = result.Data.StartDate,
            EndDate = result.Data.EndDate,
            Attachment = result.Data.Attachment,
            Status = result.Data.Status,
            LeaveName = result.Data.LeaveName,

        };


        return View(leaveRequest);
    }

    [HttpPost]
    public async Task<IActionResult> ApproveStatuss(UpdateStatusRequestVM updateStatus)
    {
        var result = await _repository.ApproveStatus(updateStatus);
        if (result.Code == 200)
        {
            return Redirect(nameof(GetByManager));
        }
        else if (result.Status == "409")
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetByEmployee()
    {
        try
        {
            var result = await _repository.GetByEmployee();
            var istoryReject = new List<LeaveRequestVM>();

            if (result.Data != null)
            {
                istoryReject = result.Data.ToList();
            }
            return View(istoryReject);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        try
        {
            var result = await _repository.Delete(guid);

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

        return RedirectToAction(nameof(GetByEmployee));
    }

}

