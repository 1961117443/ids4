using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hello.IService;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hello.IdentityServer.Controllers
{  
    [Route("v1/myAccount")]
    public class MyAccountController : Controller
    {
        private readonly IAdminService adminService;

        public MyAccountController(IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events, IAdminService adminService)
        {
            this.adminService = adminService;
        }
        /// <summary>
        /// 登录页面
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login1(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return await Task.FromResult(View());
        }

        /// <summary>
        /// 登录post回发处理
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            var user = await adminService.GetUser(userName, password);
            if (user != null)
            {
                AuthenticationProperties props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromSeconds(20))
                }; 
                await HttpContext.SignInAsync(user.ID.ToString(),user.Name,props);
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }

                return View();
            }
            else
            {
                return View();
            }
        }
    }
}