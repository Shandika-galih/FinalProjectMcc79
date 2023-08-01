using Client.Contract;
using Client.ViewModels.LeaveRequest;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Client.Controllers;

public class ManagerController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IManagerRepository _managerRepository;

    public ManagerController(IEmployeeRepository employeeRepository, ILeaveRequestRepository leaveRequestRepository, IManagerRepository managerRepository)
    {
        _employeeRepository = employeeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _managerRepository = managerRepository;
    }

    /*[HttpGet]
    public async Task<IActionResult> GetRequest(Guid managerGuid)
    {
        var result = await _managerRepository.Get(managerGuid);
        var listRequests = new List<LeaveRequestVM>();

        if (result.Data != null)
        {
            listRequests = result.Data.ToList();
        }

        return View(listRequests);
    }*/
}
