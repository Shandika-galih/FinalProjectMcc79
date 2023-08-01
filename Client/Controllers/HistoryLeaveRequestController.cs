using Client.Contract;
using Client.ViewModels.LeaveHistory;
using Client.ViewModels.LeaveRequest;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class HistoryLeaveRequestController : Controller

{
    private readonly IHistoryLeaveRequestRepository _history;

    public HistoryLeaveRequestController(IHistoryLeaveRequestRepository historyLeaveRequestRepository)
    {
        _history = historyLeaveRequestRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var result = await _history.GetLeaveHistory();
            var LeaveHistory = new List<HistoryVM>();

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

    [HttpGet]
    public async Task<IActionResult> Approve()
    {
        try
        {
            var result = await _history.GetLeaveHistoryApprove();
            var HistoryApprove = new List<HistoryVM>();

            if (result.Data != null)
            {
                HistoryApprove = result.Data.ToList();
            }
            return View(HistoryApprove);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Pending()
    {
        try
        {
            var result = await _history.GetLeaveHistoryPending();
            var HistoryPending = new List<HistoryVM>();

            if (result.Data != null)
            {
                HistoryPending = result.Data.ToList();
            }
            return View(HistoryPending);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Reject()
    {
        try
        {
            var result = await _history.GetLeaveHistoryReject();
            var istoryReject = new List<HistoryVM>();

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

    [HttpGet]
    public async Task<IActionResult> byNik()
    {
        try
        {
            var result = await _history.GetLeaveHistorybyNik();
            var istoryReject = new List<HistoryVM>();

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

    [HttpGet]
    public async Task<IActionResult> byManager()
    {
        try
        {
            var result = await _history.GetLeaveHistorybyManager();
            var nymanager = new List<HistoryVM>();

            if (result.Data != null)
            {
                nymanager = result.Data.ToList();
            }
            return View(nymanager);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }
}
