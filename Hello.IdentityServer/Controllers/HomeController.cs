using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hello.IdentityServer.Models;
using Hello.IService;

namespace Hello.IdentityServer.Controllers
{
    [Route("v1/home")]
    public class HomeController : Controller
    {
        private readonly IAdminService adminService;

        public IActionResult Index()
        {
            return View();
        }

        public HomeController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<IActionResult> Privacy()
        {
            var user = await adminService.GetUser("admin", "admin");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
