using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hello.MvcClient.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http; 
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;

namespace Hello.MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            var res = await HttpContext.AuthenticateAsync();
            var token = await HttpContext.GetTokenAsync("access_token");
            //var token = res.Properties.GetString("id_token");
            foreach (var prop in res.Properties.Items)
            {
                Console.WriteLine($"key:{prop.Key}--value:{prop.Value}");
                 
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}
