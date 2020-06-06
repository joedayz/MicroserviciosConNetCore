using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUIService.Models;

namespace WebUIService.Controllers
{
    public class AccountController : BaseController
    {
      
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            UserModel data = new UserModel();
            string str = JsonConvert.SerializeObject(model);
            StringContent strContent = new StringContent(str, Encoding.UTF8, "application/json");
            var response = client.PostAsync(client.BaseAddress + "authentication", strContent).Result;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<UserModel>(content);

                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddMinutes(120);
                HttpContext.Response.Cookies.Append("token", data.Token, options);

                if (data.Roles.Contains("User"))
                {
                    return RedirectToAction("Index", "Home", new { area = "User" });
                }
            }
            return View();
        }
    }
}