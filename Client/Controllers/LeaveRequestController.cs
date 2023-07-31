using Client.Contract;
using Client.Repositories;
using Client.ViewModels.Employee;
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

    public LeaveRequestController(ILeaveRequestRepository repository, IEmployeeRepository employeeRepository, ILeaveTypeRepository leaveType)
    {
        _repository = repository;
        _employeeRepository = employeeRepository;
        _leaveTypeRepository = leaveType;
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

        var resultLeaveType = await _leaveTypeRepository.Get();
        var listLeaveTypes = new List<LeaveTypeVM>();

        if (resultLeaveType.Data != null)
        {
            listLeaveTypes = resultLeaveType.Data.ToList();
        }
        // add to view data
        ViewData["LeaveTypes"] = listLeaveTypes;
        return View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(LeaveRequestVM leaveRequest)
	{
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
}
