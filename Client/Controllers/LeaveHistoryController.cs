using Client.Contract;
using Client.ViewModels.LeaveHistory;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Client.Controllers;

public class LeaveHistoryController : Controller
{
    private readonly ILeaveHistoryRepository _history;

    public LeaveHistoryController(ILeaveHistoryRepository leaveRequestHistory)
    {
        _history = leaveRequestHistory;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var result = await _history.GetLeaveHistory();
            var LeaveHistory = new List<LeaveHistoryEmployeeVM>();

            if (result.Data != null)
            {
                LeaveHistory = result.Data.ToList();
            }
            return View(LeaveHistory);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    public async Task<IActionResult> GetbyEmail()
    {
        try
        {
            var result = await _history.GetLeaveHistoryEmployee();
            var LeaveHistory = new List<LeaveHistoryEmployeeVM>();

            if (result.Data != null)
            {
                LeaveHistory = result.Data.ToList();
            }
            return View(LeaveHistory);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }


}
