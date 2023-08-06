using Client.Contract;
using Client.Models;
using Client.ViewModels.LeaveType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILeaveTypeRepository repository;

        public HomeController(ILogger<HomeController> logger, ILeaveTypeRepository repository)
        {
            _logger = logger;
            this.repository = repository;
        }
        [Authorize]
  /*      public IActionResult Index()
        {
            return View();
        }*/

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

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet("/Unauthorized")]
        public IActionResult Unauthorized()
        {
            return View("401");
        }

        [AllowAnonymous]
        [Route("/NotFound")]
        public IActionResult Notfound()
        {
            return View("404");
        }

        [AllowAnonymous]
        [Route("/Forbidden")]
        public IActionResult Forbidden()
        {
            return View("403");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}