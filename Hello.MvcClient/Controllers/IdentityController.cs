using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hello.MvcClient.Controllers
{
    public class IdentityController : Controller
    {
        //[Authorize]
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {

                var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
                if (disco.IsError)
                {
                    Console.WriteLine(disco.Error);
                }
                else
                {
                    var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
                    {
                        Address = disco.TokenEndpoint,
                        ClientId = "Client.MVC",
                        ClientSecret = "secret"
                    });
                    if (tokenResponse.IsError)
                    {
                        Console.WriteLine(tokenResponse.Error);
                    }
                    else
                    {
                        ViewBag.token = tokenResponse.Json;
                    }
                }
            }
            return View();
        }
         
        public IActionResult About()
        {
            return View();
        }
    }
}